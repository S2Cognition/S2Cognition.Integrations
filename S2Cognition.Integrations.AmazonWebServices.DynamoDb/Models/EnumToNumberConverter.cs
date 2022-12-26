using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace S2Cognition.Integrations.AmazonWebServices.DynamoDb.Models;

// Converts the complex type DimensionType to string and vice-versa.
public class EnumToNumberConverter<T> : IPropertyConverter
    where T : Enum
{
    public DynamoDBEntry ToEntry(object? value)
    {
        if (value == null)
        {
            return new Primitive
            {
                Type = DynamoDBEntryType.Numeric,
                Value = null
            };
        }

        return new Primitive
        {
            Type = DynamoDBEntryType.Numeric,
            Value = (int)value
        };
    }

    public object? FromEntry(DynamoDBEntry entry)
    {
        if (entry is Primitive primitive && primitive.Value != null)
            return (T)primitive.Value;

        return null;
    }
}
