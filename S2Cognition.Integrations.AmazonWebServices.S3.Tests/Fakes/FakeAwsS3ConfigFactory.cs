using S2Cognition.Integrations.AmazonWebServices.S3.Models;

namespace S2Cognition.Integrations.AmazonWebServices.S3.Tests.Fakes;

internal class FakeAwsS3ConfigFactory : IAwsS3ConfigFactory
{
    internal FakeAwsS3ConfigFactory()
    {
    }

    public IAwsS3Config Create()
    {
        return new FakeAwsS3Config();
    }
}
