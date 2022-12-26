using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

public interface ICreateItemRequest : IMondayRequest
{
    string? Name { get; set; }
    ulong? BoardId { get; set; }
    string? GroupId { get; set; }
    IMondayColumns? ColumnValues { get; set; }
}

public class CreateItemRequest : MondayRequest, ICreateItemRequest
{
    public string? Name { get; set; }
    public ulong? BoardId { get; set; }
    public string? GroupId { get; set; }
    public IMondayColumns? ColumnValues { get; set; }

    public ColumnOptions ColumnOptions { get; set; }

    public CreateItemRequest()
    {
        ColumnOptions = new ColumnOptions(RequestMode.Default);
    }

    public CreateItemRequest(RequestMode mode)
        : this()
    {
        ColumnOptions = new ColumnOptions(mode);
    }
}

public interface ICreateItemResult : IMondayResult
{
    Item? Data { get; set; }
}

public class CreateItemResult : MondayResult, ICreateItemResult
{
    public Item? Data { get; set; }
}

public class CreateSubItemRequest : MondayRequest, IMondayRequest
{
    public ulong? ParentId { get; set; }
    public string? ItemName { get; set; }
}

public class CreateSubItemResponse
{
}

public class CreateSubItemResult : MondayResult, IMondayResult
{
}
