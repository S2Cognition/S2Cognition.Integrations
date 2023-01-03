using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.Core.Data;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public interface IAmazonWebServicesDynamoDbIntegration : IIntegration<AmazonWebServicesDynamoDbConfiguration>
{
    Task Create<T>(T data);
    Task Create(Type dataType, object data);
    Task Create(Type dataType, IEnumerable<object> data);
    
    Task<T?> Read<T>(T data);
}

public class AmazonWebServicesDynamoDbIntegration : Integration<AmazonWebServicesDynamoDbConfiguration>, IAmazonWebServicesDynamoDbIntegration
{
    public AmazonWebServicesDynamoDbIntegration(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    private IAwsDynamoDbContext? _context = null;
    private async Task<IAwsDynamoDbContext> DbContext()
    {
        if (_context == null)
        {
            var clientConfigFactory = _ioc.GetRequiredService<IAwsDynamoDbConfigFactory>();
            var clientConfig = clientConfigFactory.Create();

            if (!String.IsNullOrWhiteSpace(Configuration.ServiceUrl))
                clientConfig.ServiceUrl = Configuration.ServiceUrl;

            if (!String.IsNullOrWhiteSpace(Configuration.AwsRegion))
            {
                var regionUtil = _ioc.GetRequiredService<IAwsRegionFactory>();
                clientConfig.RegionEndpoint = await regionUtil.Create(Configuration.AwsRegion);
            }

            var clientFactory = _ioc.GetRequiredService<IAwsDynamoDbClientFactory>();
            var client = clientFactory.Create(clientConfig);

            var contextFactory = _ioc.GetRequiredService<IAwsDynamoDbContextFactory>();
            _context = contextFactory.Create(client);
        }

        return await Task.FromResult(_context);
    }

    public async Task Create<T>(T data)
    {
        var context = await DbContext();
        await context.Save(data);
    }

    public async Task Create(Type dataType, object data)
    {
        var context = await DbContext();
        await context.Save(dataType, data);
    }

    public async Task Create(Type dataType, IEnumerable<object> data)
    {
        var context = await DbContext();
        await context.Save(dataType, data);
    }

    public async Task<T?> Read<T>(T data)
    {
        var context = await DbContext();
        return await context.Load(data);
    }
}
