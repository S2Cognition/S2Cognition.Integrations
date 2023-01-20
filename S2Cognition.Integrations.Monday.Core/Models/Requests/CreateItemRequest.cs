using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface ICreateItemRequest : IMondayRequest
{
    string? Name { get; set; }
    ulong? BoardId { get; set; }
    string? GroupId { get; set; }
    IMondayColumns? ColumnValues { get; set; }
}

internal class CreateItemRequest : MondayRequest, ICreateItemRequest
{
    public string? Name { get; set; }
    public ulong? BoardId { get; set; }
    public string? GroupId { get; set; }
    public IMondayColumns? ColumnValues { get; set; }

    public ColumnOptions ColumnOptions { get; set; }

    internal CreateItemRequest()
    {
        ColumnOptions = new ColumnOptions(RequestMode.Default);
    }

    internal CreateItemRequest(RequestMode mode)
        : this()
    {
        ColumnOptions = new ColumnOptions(mode);
    }
}

internal interface ICreateItemResult : IMondayResult
{
    Item? Data { get; set; }
}

internal class CreateItemResult : MondayResult, ICreateItemResult
{
    public Item? Data { get; set; }
}

internal class CreateSubItemRequest : MondayRequest, IMondayRequest
{
    public ulong? ParentId { get; set; }
    public string? ItemName { get; set; }
}

internal class CreateSubItemResponse
{
}

internal class CreateSubItemResult : MondayResult, IMondayResult
{
}
