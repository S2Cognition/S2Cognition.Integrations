namespace S2Cognition.Integrations.AmazonWebServices.S3.Models;

internal interface IAwsS3ConfigFactory
{
    IAwsS3Config Create();
}
internal class AwsS3ConfigFactory : IAwsS3ConfigFactory
{
    public IAwsS3Config Create()
    {
        return new AwsS3Config();
    }
}
