using Amazon.DynamoDBv2.DataModel;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

internal interface IAwsDynamoDbContext
{
    Task<T?> Load<T>(T? data);

    Task Save<T>(T? data);

#if AWS_SUPPORTS_NONGENERIC_DYNAMODB
    Task Save(Type dataType, object data);
    Task Save(Type dataType, IEnumerable<object> data);
#endif 
}

internal class AwsDynamoDbContext : IAwsDynamoDbContext
{
    private readonly DynamoDBContext _context;

    internal AwsDynamoDbContext(IAwsDynamoDbClient client)
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


