﻿using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Models;

internal interface IAwsS3Client
{

    Task<DownloadS3FileResponse> DownloadFileAsync(DownloadS3FileRequest req);
    Task<UploadS3FileResponse> UploadFileAsync(UploadS3FileRequest req);
    AmazonS3Client Native { get; }

}
internal class AwsS3Client : IAwsS3Client
{
    private readonly AmazonS3Client _client;

    public AmazonS3Client Native => _client;

    internal AwsS3Client(IAwsS3Config config)
    {
        _client = new AmazonS3Client(config.Native);
    }

    public async Task<DownloadS3FileResponse> DownloadFileAsync(DownloadS3FileRequest req)
    {
        if (req.BucketName == null ||
            req.FileName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        if (req.RequestType == DownloadFileRequestType.RawData)
        {
            return await ProcessRawDataRequest(req);
        }
        else
        {
            return await ProcessSignedURLRequest(req);
        }
    }

    public async Task<UploadS3FileResponse> UploadFileAsync(UploadS3FileRequest req)
    {
        using var transferUtil = new TransferUtility(_client);

        if (req.FileData == null ||
            req.FileName == null ||
            req.BucketName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        using var memoryStream = new MemoryStream(req.FileData);

        var transferUtilityUploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = memoryStream,
            BucketName = req.BucketName,
            Key = req.FileName,
            CannedACL = S3CannedACL.Private,
        };
        try
        {
            await transferUtil.UploadAsync(transferUtilityUploadRequest);
            return new UploadS3FileResponse();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Aws S3 File Upload Failed {ex.Message}");
        }

    }

    private async Task<DownloadS3FileResponse> ProcessSignedURLRequest(DownloadS3FileRequest req)
    {
        try
        {
            var response = Native.GetPreSignedURL(new GetPreSignedUrlRequest
            {
                Expires = DateTime.UtcNow.AddDays(6),
                BucketName = req.BucketName,
                Key = req.FileName
            });

            var returnValue = new DownloadS3FileResponse
            {
                FileData = null,
                SignedURL = response
            };

            return await Task.FromResult(returnValue);
        }
        catch (Exception ex)
        {
            throw new InvalidDataException($"Invalid Response from Server {ex.Message}");
        }
    }

    private async Task<DownloadS3FileResponse> ProcessRawDataRequest(DownloadS3FileRequest req)
    {
        using var transferUtil = new TransferUtility(_client);

        try
        {
            var response = await transferUtil.OpenStreamAsync(new TransferUtilityOpenStreamRequest
            {
                BucketName = req.BucketName,
                Key = req.FileName
            });

            if (response != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    response.CopyTo(memoryStream);

                    return new DownloadS3FileResponse()
                    {
                        FileData = memoryStream.ToArray(),
                        SignedURL = null
                    };
                }
            }
        }
        catch (Exception ex)
        {
            throw new InvalidDataException($"Invalid Response from Server {ex.Message}");
        }

        return new DownloadS3FileResponse()
        {
            FileData = Array.Empty<byte>(),
            SignedURL = null
        };
    }
}

