using Amazon.S3;
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
        if (String.IsNullOrWhiteSpace(req.BucketName))
            throw new ArgumentException(nameof(DownloadS3FileRequest.BucketName));

        if (String.IsNullOrWhiteSpace(req.FileName))
            throw new ArgumentException(nameof(DownloadS3FileRequest.FileName));

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

        if ((req.FileData == null) || (req.FileData.Length < 1))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileData));

        if (String.IsNullOrWhiteSpace(req.FileName))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileName));

        if (String.IsNullOrWhiteSpace(req.BucketName))
            throw new ArgumentException(nameof(UploadS3FileRequest.BucketName));

        using var memoryStream = new MemoryStream(req.FileData);

        var transferUtilityUploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = memoryStream,
            BucketName = req.BucketName,
            Key = req.FileName,
            CannedACL = S3CannedACL.Private,
        };

        await transferUtil.UploadAsync(transferUtilityUploadRequest);
        return new UploadS3FileResponse();
    }

    private async Task<DownloadS3FileResponse> ProcessSignedURLRequest(DownloadS3FileRequest req)
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

    private async Task<DownloadS3FileResponse> ProcessRawDataRequest(DownloadS3FileRequest req)
    {
        using var transferUtil = new TransferUtility(_client);
        var response = await transferUtil.OpenStreamAsync(new TransferUtilityOpenStreamRequest
        {
            BucketName = req.BucketName,
            Key = req.FileName
        });

        if (response != null)
        {
            using var memoryStream = new MemoryStream();
            response.CopyTo(memoryStream);

            return new DownloadS3FileResponse()
            {
                FileData = memoryStream.ToArray(),
                SignedURL = null
            };
        }

        return new DownloadS3FileResponse()
        {
            FileData = Array.Empty<byte>(),
            SignedURL = null
        };
    }
}


