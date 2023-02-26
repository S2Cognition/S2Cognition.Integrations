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

        var testResponse = req.RequestType switch
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
            _ => throw new ArgumentException(nameof(DownloadS3FileRequest.RequestType))
        };

        return await Task.FromResult(testResponse);

    }
    public async Task<UploadS3FileResponse> UploadFileAsync(UploadS3FileRequest req)
    {
        if ((req.FileData == null) || (req.FileData.Length  <1))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileData));

        if (String.IsNullOrWhiteSpace(req.FileName))
            throw new ArgumentException(nameof(UploadS3FileRequest.FileName));

        if (String.IsNullOrWhiteSpace(req.BucketName))
            throw new ArgumentException(nameof(UploadS3FileRequest.BucketName));

        return await Task.FromResult(new UploadS3FileResponse());
    }
}
