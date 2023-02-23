using S2Cognition.Integrations.AmazonWebServices.S3.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;

internal class FakeAwsS3ClientFactory : IAwsS3ClientFactory
{
    private FakeAwsS3Client? _client = null;

    public IAwsS3Client Create(IAwsS3Config config)
    {
        if (_client == null)
        {
            _client = new FakeAwsS3Client();
        }
        return _client;
    }
}

