using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using S2Cognition.Integrations.AmazonWebServices.DynamoDb.Data;
using S2Cognition.Integrations.Core.Tests;
using Shouldly;

namespace S2Cognition.Integrations.AmazonWebServices.Core.Tests;

public class DynamoDbTests : UnitTestBase
{
    private AmazonWebServicesDynamoDbConfiguration _configuration = default!;
    private IAmazonWebServicesDynamoDbIntegration _sut = default!;

    protected override async Task IocSetup(IServiceCollection sc)
    {
        sc.AddAmazonWebServicesDynamoDbIntegration();
        await Task.CompletedTask;
    }

    protected override async Task TestSetup()
    {
        _configuration = new AmazonWebServicesDynamoDbConfiguration(_ioc)
        {
            AccessKey = "fake AccessKey",
            SecretKey = "fake SecretKey",
            AwsRegion = "fake AwsRegion",
            ServiceUrl = "fake ServiceUrl"
        };

        _sut = _ioc.GetRequiredService<IAmazonWebServicesDynamoDbIntegration>();
        await _sut.Initialize(_configuration);
    }

    public enum RowStatuses
    {
        Normal = 0,
        Deleted = 1
    }

    [DynamoDBTable("RandomTable")]
    public class TestTable
    {
        [DynamoDBHashKey("Id")]
        public int? Id { get; set; }
        [DynamoDBProperty("AlternateName")]
        public string? Name { get; set; }
        [DynamoDBProperty("Notes")]
        public string? Notes { get; set; }
        [DynamoDBProperty("UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }
        [DynamoDBProperty("UpdatedBy")]
        public int? UpdatedBy { get; set; }
        [DynamoDBProperty("RowState", Converter = typeof(EnumToNumberConverter<RowStatuses>))]
        public RowStatuses? RowState { get; set; }
    }

    [Fact]
    public async Task EnsureCanInsertNewItem()
    {
        var id = 1000;
        var name = "Item Name";
        var notes = "Item Notes";
        var updatedOn = DateTime.UtcNow;
        var updatedBy = 213;
        var rowState = RowStatuses.Normal;

        var newRow = new TestTable
        {
            Id = id,
            Name = name,
            Notes = notes,
            UpdatedOn = updatedOn,
            UpdatedBy = updatedBy,
            RowState = rowState
        };
        await _sut.Create(newRow);

        var queryRow = new TestTable
        {
            Id = id
        };
        var result = await _sut.Read(queryRow);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(id);
        result.Name.ShouldBe(name);
        result.Notes.ShouldBe(notes);
        result.UpdatedOn.ShouldBe(updatedOn);
        result.UpdatedBy.ShouldBe(updatedBy);
        result.RowState.ShouldBe(rowState);
    }
}