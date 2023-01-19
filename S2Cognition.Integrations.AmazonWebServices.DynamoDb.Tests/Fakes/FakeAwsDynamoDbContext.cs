using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Tests.Fakes;

internal class FakeAwsDynamoDbContext : IAwsDynamoDbContext
{
    private readonly ICollection<object> _data = new List<object>();

    public async Task<T?> Load<T>(T? data)
    {
        // This isn't going to cut it...
        return await Task.FromResult((T)_data.Last());
    }

    public async Task Save<T>(T? data)
    {
        if (data != null)
            _data.Add(data);

        await Task.CompletedTask;
    }

#if AWS_SUPPORTS_NONGENERIC_DYNAMODB
    public async Task Save(Type dataType, object data)
    {
        if (data != null)
            _data.Add(data);

        await Task.CompletedTask;
    }

    public async Task Save(Type dataType, IEnumerable<object> data)
    {
        if (data != null)
        {
            foreach (var item in data)
                await Save(dataType, item);
        }

        await Task.CompletedTask;
    }
#endif
}
