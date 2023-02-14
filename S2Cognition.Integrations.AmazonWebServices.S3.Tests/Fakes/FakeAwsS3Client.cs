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

    public void ExpectedFileContent(byte[] fileContent)
    {
        _downloadFileContent = fileContent;
    }

    public async Task<Stream> DownloadFileAsync(DownloadS3FileRequest req)
    {
        if (_downloadFileContent == null)
            throw new InvalidOperationException("Expectations were not set on fake.");

        return await Task.FromResult(new MemoryStream(_downloadFileContent));
    }
    public async Task<bool> UploadFileAsync(UploadS3FileRequest req)
    {
        if (req.FileData == null ||
            req.FileName == null ||
            req.BucketName == null)
            throw new InvalidDataException("Invalid Parameters Exception");

        return await Task.FromResult(true);
    }
}
