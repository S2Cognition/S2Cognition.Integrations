namespace S2Cognition.Integrations.AmazonWebServices.S3.Models;

internal interface IAwsS3ClientFactory
{
    IAwsS3Client Create(IAwsS3Config config);
}
internal class AwsS3ClientFactory : IAwsS3ClientFactory
{
    public IAwsS3Client Create(IAwsS3Config config)
    {
        return new AwsS3Client(config);
    }
}
