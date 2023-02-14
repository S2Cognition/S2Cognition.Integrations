using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazanWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.Core.Tests;
using S2Cognition.Integrations.AmazonWebServices.S3.Data;
using S2Cognition.Integrations.AmazonWebServices.S3.Models;
using S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;
using Xunit;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests;

public class S3Tests : UnitTestBase
{

    private IAmazonWebServicesS3Integration _sut = default!;
    private IFakeAwsS3Client _client = default!;
    private static byte[] FakeFileContents => new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25 };

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddAmazonWebServicesS3Integration();
        sc.AddFakeAmazonWebServices();
        sc.AddFakeAmazonWebServicesS3();

        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        var configuration = new AmazonWebServicesS3Configuration(_ioc)
        {
            AccessKey = "fake AccessKey",
            SecretKey = "fake SecretKey",
            AwsRegion = "fake AwsRegion"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesS3Integration>();

        await _sut.Initialize(configuration);

        var configFactory = _ioc.GetRequiredService<IAwsS3ConfigFactory>();
        var config = configFactory.Create();

        var clientFactory = _ioc.GetRequiredService<IAwsS3ClientFactory>();
        _client = clientFactory.Create(config) as IFakeAwsS3Client ?? throw new InvalidOperationException();
    }

    [Fact]
    public async Task EnsureDownloadS3FileReturnsFileContents()
    {
        var bucketName = "fake Bucket Name";
        var key = "fake key";

        _client.ExpectedFileContent(FakeFileContents);

        var response = await _sut.DownloadS3File(new DownloadS3FileRequest
        {
            BucketName = bucketName,
            FileName = key
        });

        response.ShouldNotBeNull();
        response.Length.ShouldBeEquivalentTo(6);
        response.ShouldBe(FakeFileContents);

        await Task.CompletedTask;
    }

    [Fact]
    public async Task EnsureUploadS3FileReturns()
    {
        var bucketName = "fake Bucket Name";
        var fileName = "fakeFile.txt";

        var fakeFileContents = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25 };

        _client.ExpectedFileContent(FakeFileContents);

        var response = await _sut.UploadS3File(new UploadS3FileRequest
        {
            FileData = fakeFileContents,
            BucketName = bucketName,
            FileName = fileName
        });

        response.ShouldBeTrue();
        await Task.CompletedTask;
    }
}
