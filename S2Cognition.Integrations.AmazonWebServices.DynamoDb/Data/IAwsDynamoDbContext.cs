namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;

public interface IAwsDynamoDbContext
{
    Task<T?> Load<T>(T? data);

    Task Save<T>(T? data);

#if AWS_SUPPORTS_NONGENERIC_DYNAMODB
    Task Save(Type dataType, object data);
    Task Save(Type dataType, IEnumerable<object> data);
#endif 
}
