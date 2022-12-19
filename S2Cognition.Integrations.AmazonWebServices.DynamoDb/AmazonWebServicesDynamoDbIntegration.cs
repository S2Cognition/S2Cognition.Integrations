using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;
using S2Cognition.Integrations.Core;

namespace S2Cognition.Integrations.AmazonWebServices.Core;

public interface IAmazonWebServicesDynamoDbIntegration : IIntegration<AmazonWebServicesDynamoDbConfiguration>
{
    Task Create<T>(T data);
    Task<T> Read<T>(T data);
}

internal class AmazonWebServicesDynamoDbIntegration : Integration<AmazonWebServicesDynamoDbConfiguration>, IAmazonWebServicesDynamoDbIntegration
{
    private async Task<DynamoDBContext> DbContext()
    {
        var clientConfig = new AmazonDynamoDBConfig();
        if (!String.IsNullOrWhiteSpace(Configuration.ServiceUrl))
            clientConfig.ServiceURL = Configuration.ServiceUrl;

        if (!String.IsNullOrWhiteSpace(Configuration.AwsRegion))
            clientConfig.RegionEndpoint = RegionEndpoint.GetBySystemName(Configuration.AwsRegion);

        var client = new AmazonDynamoDBClient(clientConfig);

        return await Task.FromResult(new DynamoDBContext(client));
    }

    public async Task Create<T>(T data)
    {
        using var context = await DbContext();
        await context.SaveAsync(data);
    }

    public async Task<T> Read<T>(T data)
    {
        using var context = await DbContext();
        return await context.LoadAsync(data);
    }
    /*
using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.SecurityToken;

namespace com.amazonaws.codesamples
{
class LowLevelItemCRUDExample
{
    private static string tableName = "ProductCatalog";
    private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();

    static void Main(string[] args)
    {
        try
        {
            CreateItem();
            RetrieveItem();

            // Perform various updates.
            UpdateMultipleAttributes();
            UpdateExistingAttributeConditionally();

            // Delete item.
            DeleteItem();
            Console.WriteLine("To continue, press Enter");
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("To continue, press Enter");
            Console.ReadLine();
        }
    }

    private static void CreateItem()
    {
        var request = new PutItemRequest
        {
            TableName = tableName,
            Item = new Dictionary<string, AttributeValue>()
        {
            { "Id", new AttributeValue {
                  N = "1000"
              }},
            { "Title", new AttributeValue {
                  S = "Book 201 Title"
              }},
            { "ISBN", new AttributeValue {
                  S = "11-11-11-11"
              }},
            { "Authors", new AttributeValue {
                  SS = new List<string>{"Author1", "Author2" }
              }},
            { "Price", new AttributeValue {
                  N = "20.00"
              }},
            { "Dimensions", new AttributeValue {
                  S = "8.5x11.0x.75"
              }},
            { "InPublication", new AttributeValue {
                  BOOL = false
              } }
        }
        };
        client.PutItem(request);
    }

    private static void RetrieveItem()
    {
        var request = new GetItemRequest
        {
            TableName = tableName,
            Key = new Dictionary<string, AttributeValue>()
        {
            { "Id", new AttributeValue {
                  N = "1000"
              } }
        },
            ProjectionExpression = "Id, ISBN, Title, Authors",
            ConsistentRead = true
        };
        var response = client.GetItem(request);

        // Check the response.
        var attributeList = response.Item; // attribute list in the response.
        Console.WriteLine("\nPrinting item after retrieving it ............");
        PrintItem(attributeList);
    }

    private static void UpdateMultipleAttributes()
    {
        var request = new UpdateItemRequest
        {
            Key = new Dictionary<string, AttributeValue>()
        {
            { "Id", new AttributeValue {
                  N = "1000"
              } }
        },
            // Perform the following updates:
            // 1) Add two new authors to the list
            // 1) Set a new attribute
            // 2) Remove the ISBN attribute
            ExpressionAttributeNames = new Dictionary<string, string>()
        {
            {"#A","Authors"},
            {"#NA","NewAttribute"},
            {"#I","ISBN"}
        },
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
        {
            {":auth",new AttributeValue {
                 SS = {"Author YY", "Author ZZ"}
             }},
            {":new",new AttributeValue {
                 S = "New Value"
             }}
        },

            UpdateExpression = "ADD #A :auth SET #NA = :new REMOVE #I",

            TableName = tableName,
            ReturnValues = "ALL_NEW" // Give me all attributes of the updated item.
        };
        var response = client.UpdateItem(request);

        // Check the response.
        var attributeList = response.Attributes; // attribute list in the response.
                                                 // print attributeList.
        Console.WriteLine("\nPrinting item after multiple attribute update ............");
        PrintItem(attributeList);
    }

    private static void UpdateExistingAttributeConditionally()
    {
        var request = new UpdateItemRequest
        {
            Key = new Dictionary<string, AttributeValue>()
        {
            { "Id", new AttributeValue {
                  N = "1000"
              } }
        },
            ExpressionAttributeNames = new Dictionary<string, string>()
        {
            {"#P", "Price"}
        },
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
        {
            {":newprice",new AttributeValue {
                 N = "22.00"
             }},
            {":currprice",new AttributeValue {
                 N = "20.00"
             }}
        },
            // This updates price only if current price is 20.00.
            UpdateExpression = "SET #P = :newprice",
            ConditionExpression = "#P = :currprice",

            TableName = tableName,
            ReturnValues = "ALL_NEW" // Give me all attributes of the updated item.
        };
        var response = client.UpdateItem(request);

        // Check the response.
        var attributeList = response.Attributes; // attribute list in the response.
        Console.WriteLine("\nPrinting item after updating price value conditionally ............");
        PrintItem(attributeList);
    }

    private static void DeleteItem()
    {
        var request = new DeleteItemRequest
        {
            TableName = tableName,
            Key = new Dictionary<string, AttributeValue>()
        {
            { "Id", new AttributeValue {
                  N = "1000"
              } }
        },

            // Return the entire item as it appeared before the update.
            ReturnValues = "ALL_OLD",
            ExpressionAttributeNames = new Dictionary<string, string>()
        {
            {"#IP", "InPublication"}
        },
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
        {
            {":inpub",new AttributeValue {
                 BOOL = false
             }}
        },
            ConditionExpression = "#IP = :inpub"
        };

        var response = client.DeleteItem(request);

        // Check the response.
        var attributeList = response.Attributes; // Attribute list in the response.
                                                 // Print item.
        Console.WriteLine("\nPrinting item that was just deleted ............");
        PrintItem(attributeList);
    }

    private static void PrintItem(Dictionary<string, AttributeValue> attributeList)
    {
        foreach (KeyValuePair<string, AttributeValue> kvp in attributeList)
        {
            string attributeName = kvp.Key;
            AttributeValue value = kvp.Value;

            Console.WriteLine(
                attributeName + " " +
                (value.S == null ? "" : "S=[" + value.S + "]") +
                (value.N == null ? "" : "N=[" + value.N + "]") +
                (value.SS == null ? "" : "SS=[" + string.Join(",", value.SS.ToArray()) + "]") +
                (value.NS == null ? "" : "NS=[" + string.Join(",", value.NS.ToArray()) + "]")
                );
        }
        Console.WriteLine("************************************************");
    }
}
}



private static void TestBatchWrite()
    {
        var request = new BatchWriteItemRequest
        {
            ReturnConsumedCapacity = "TOTAL",
            RequestItems = new Dictionary<string, List<WriteRequest>>
        {
            {
                table1Name, new List<WriteRequest>
                {
                    new WriteRequest
                    {
                        PutRequest = new PutRequest
                        {
                            Item = new Dictionary<string, AttributeValue>
                            {
                                { "Name", new AttributeValue {
                                      S = "S3 forum"
                                  } },
                                { "Threads", new AttributeValue {
                                      N = "0"
                                  }}
                            }
                        }
                    }
                }
            },
            {
                table2Name, new List<WriteRequest>
                {
                    new WriteRequest
                    {
                        PutRequest = new PutRequest
                        {
                            Item = new Dictionary<string, AttributeValue>
                            {
                                { "ForumName", new AttributeValue {
                                      S = "S3 forum"
                                  } },
                                { "Subject", new AttributeValue {
                                      S = "My sample question"
                                  } },
                                { "Message", new AttributeValue {
                                      S = "Message Text."
                                  } },
                                { "KeywordTags", new AttributeValue {
                                      SS = new List<string> { "S3", "Bucket" }
                                  } }
                            }
                        }
                    },
                    new WriteRequest
                    {
                        // For the operation to delete an item, if you provide a primary key value
                        // that does not exist in the table, there is no error, it is just a no-op.
                        DeleteRequest = new DeleteRequest
                        {
                            Key = new Dictionary<string, AttributeValue>()
                            {
                                { "ForumName",  new AttributeValue {
                                      S = "Some partition key value"
                                  } },
                                { "Subject", new AttributeValue {
                                      S = "Some sort key value"
                                  } }
                            }
                        }
                    }
                }
            }
        }
        };

        CallBatchWriteTillCompletion(request);
    }

    private static void CallBatchWriteTillCompletion(BatchWriteItemRequest request)
    {
        BatchWriteItemResponse response;

        int callCount = 0;
        do
        {
            Console.WriteLine("Making request");
            response = client.BatchWriteItem(request);
            callCount++;

            // Check the response.

            var tableConsumedCapacities = response.ConsumedCapacity;
            var unprocessed = response.UnprocessedItems;

            Console.WriteLine("Per-table consumed capacity");
            foreach (var tableConsumedCapacity in tableConsumedCapacities)
            {
                Console.WriteLine("{0} - {1}", tableConsumedCapacity.TableName, tableConsumedCapacity.CapacityUnits);
            }

            Console.WriteLine("Unprocessed");
            foreach (var unp in unprocessed)
            {
                Console.WriteLine("{0} - {1}", unp.Key, unp.Value.Count);
            }
            Console.WriteLine();

            // For the next iteration, the request will have unprocessed items.
            request.RequestItems = unprocessed;
        } while (response.UnprocessedItems.Count > 0);

        Console.WriteLine("Total # of batch write API calls made: {0}", callCount);
    }
}
    */
}

// Converts the complex type DimensionType to string and vice-versa.
public class EnumToNumberConverter<T> : IPropertyConverter
    where T : Enum
{
    public DynamoDBEntry ToEntry(object value)
    {
        return new Primitive
        {
            Type = DynamoDBEntryType.Numeric,
            Value = (int)value
        };
    }

    public object FromEntry(DynamoDBEntry entry)
    {
        if (entry is Primitive primitive)
        {
            return (T)primitive.Value;
        }

        return default;
    }
}