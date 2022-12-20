using Amazon.DynamoDBv2.DataModel;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb;

public class AwsDynamoDbContext : IAwsDynamoDbContext
{
    private readonly DynamoDBContext _context;

    public AwsDynamoDbContext(IAwsDynamoDbClient client)
    {
        _context = new DynamoDBContext(client.Native);
    }

    public async Task<T?> Load<T>(T? data)
    {
        if (data != null)
        {
            return await _context.LoadAsync(data);
        }

        return default;
    }

    public async Task Save<T>(T? data)
    {
        if (data != null)
        {
            await _context.SaveAsync(data);
        }
    }
}


