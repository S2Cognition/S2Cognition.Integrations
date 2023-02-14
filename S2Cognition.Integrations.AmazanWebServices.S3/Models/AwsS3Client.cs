using Amazon.S3;
using Amazon.S3.Transfer;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Models;

internal interface IAwsS3Client
{

    Task<Stream> DownloadFileAsync(DownloadS3FileRequest req);
    Task<bool> UploadFileAsync(UploadS3FileRequest req);
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

    public async Task<Stream> DownloadFileAsync(DownloadS3FileRequest req)
    {
        if (req.BucketName == null ||
            req.FileName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        using var transferUtil = new TransferUtility(_client);

        var response = await transferUtil.OpenStreamAsync(new TransferUtilityOpenStreamRequest
        {
            BucketName = req.BucketName,
            Key = req.FileName
        });

        return response;
    }

    public async Task<bool> UploadFileAsync(UploadS3FileRequest req)
    {

        using var transferUtil = new TransferUtility(_client);

        if (req.FileData == null ||
            req.FileName == null ||
            req.BucketName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        var memoryStream = new MemoryStream(req.FileData);

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
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}


