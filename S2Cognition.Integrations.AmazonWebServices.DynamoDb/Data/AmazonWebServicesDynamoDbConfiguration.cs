using S2Cognition.Integrations.AmazonWebServices.Core.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public class AmazonWebServicesDynamoDbConfiguration : AmazonWebServicesConfiguration
{
    public string ServiceUrl { get; set; } = string.Empty;

        public AmazonWebServicesDynamoDbConfiguration(IServiceProvider ioc)
            : base(ioc)
    {
    }
}