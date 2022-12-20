namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbContext
{
    Task<T?> Load<T>(T? data);
    Task Save<T>(T? data);
}
