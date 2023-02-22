using Amazon.S3;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;

internal interface IFakeAwsS3Client
{
    void ExpectedFileContent(byte[] fileContent);
}
internal class FakeAwsS3Client : IAwsS3Client, IFakeAwsS3Client
{
    public AmazonS3Client Native => throw new NotImplementedException();

    private byte[]? _downloadFileContent = null;
    private const string _returnURL = "http://www.AWS.com";

    public void ExpectedFileContent(byte[] fileContent)
    {
        _downloadFileContent = fileContent;
    }

    public async Task<DownloadS3FileResponse> DownloadFileAsync(DownloadS3FileRequest req)
    {
        if (_downloadFileContent == null)
            throw new InvalidOperationException("Expectations were not set on fake.");

        DownloadS3FileResponse testResponse = new DownloadS3FileResponse();

        testResponse = req.RequestType switch
        {
            DownloadFileRequestType.SignedURL => new DownloadS3FileResponse
            {
                FileData = null,
                SignedURL = _returnURL
            },
            DownloadFileRequestType.RawData => new DownloadS3FileResponse
            {
                FileData = _downloadFileContent,
                SignedURL = null
            },
            _ => throw new InvalidDataException("Invalid Parameters Exception Download S3 File"),
        };

        return await Task.FromResult(testResponse);

    }
    public async Task<UploadS3FileResponse> UploadFileAsync(UploadS3FileRequest req)
    {
        if (req.FileData == null ||
            req.FileName == null ||
            req.BucketName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        return await Task.FromResult(new UploadS3FileResponse());
    }
}
