using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

public class AwsDynamoDbContext : IAwsDynamoDbContext
{
    private readonly DynamoDBContext _context;
    private readonly AmazonDynamoDBClient _client;

    public AwsDynamoDbContext(IAwsDynamoDbClient client)
    {
        _client = client.Native;
        _context = new DynamoDBContext(_client);
    }

    public async Task<T?> Load<T>(T? data)
    {
        if (data != null)
        {
            return await _context.LoadAsync(data);
        }

        return default;
    }

#if AWS_SUPPORTS_NONGENERIC_DYNAMODB
    public async Task Save(Type dataType, object data)
    {
        await _context.SaveAsync(dataType, data);
    }

    public async Task Save(Type dataType, IEnumerable<object> data)
    {
        var batchWrite = _context.CreateBatchWrite(dataType);
        batchWrite.AddPutItems(data);
        await batchWrite.ExecuteAsync();
    }
#endif

    public async Task Save<T>(T? data)
    {
        if (data != null)
        {
            await _context.SaveAsync(data);
        }
    }
}


