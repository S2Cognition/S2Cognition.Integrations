using S2Cognition.Integrations.Monday.Core.Models;

namespace S2Cognition.Integrations.Monday.Core.Data;

public enum ItemState
{
    Active
}

public class GetUsersResponse : List<UserRecord>
{
}

public class UserRecord
{
    public ulong Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string? Url { get; set; }
    public string? Photo { get; set; }
    public string? Title { get; set; }
    public DateTime? Birthday { get; set; }
    public string? CountryCode { get; set; }
    public string? Location { get; set; }
    public string? TimeZoneIdentifier { get; set; }
    public string? Phone { get; set; }
    public string? MobilePhone { get; set; }
    public bool IsGuest { get; set; } = true;
    public bool IsPending { get; set; } = false;
    public bool IsEnabled { get; set; } = false;
    public DateTime CreatedAt { get; set; } 
}

public class GetItemsRequest
{
    public ulong BoardId { get; set; }
    public string? Name { get; set; }
    public ItemState? State { get; set; }
}

public class GetItemsResponse : List<ItemRecord>
{
}

public class GroupRecord
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
}

public enum DataType
{
    Text,
    LongText
}

public class DynamicColumnRecord
{
    private readonly Dictionary<string, string> _columnValuesById = new();
    private readonly Dictionary<string, DataType> _columnTypesById = new();
    private readonly Dictionary<string, string> _columnValuesByName = new();
    private readonly Dictionary<string, DataType> _columnTypesByName = new();

    public ItemColumn GetColumn(string columnId)
    {
        if (_columnValuesById.TryGetValue(columnId, out string? valudById)
            && !String.IsNullOrWhiteSpace(valudById))
        {
            return new ItemColumn
            {
                HasValue = true,
                Value = valudById
            };
        }

        return new ItemColumn();
    }

    public ItemColumn GetColumnByName(string columnName)
    {
        if (_columnValuesByName.TryGetValue(columnName, out string? valueByName)
            && !String.IsNullOrWhiteSpace(valueByName))
        {
            return new ItemColumn
            {
                HasValue = true,
                Value = valueByName
            };
        }

        return new ItemColumn();
    }

    public void SetColumn(string columnId, string columnName, string value, DataType? DataType = null)
    {
        SetColumnById(columnId, value, DataType);
        SetColumnByName(columnId, value, DataType);
    }

    public void SetColumnById(string columnId, string value, DataType? DataType = null)
    {
        _columnValuesById[columnId] = value;
        if(DataType.HasValue)
            _columnTypesById[columnId] = DataType.Value;
    }

    public void SetColumnByName(string columnName, string value, DataType? DataType = null)
    {
        _columnValuesByName[columnName] = value;
        if (DataType.HasValue)
            _columnTypesByName[columnName] = DataType.Value;
    }
}

public class ItemRecord : DynamicColumnRecord
{
    public ulong Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public ItemState State { get; set; }
    public GroupRecord? Group { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ItemColumn
{
    public bool HasValue { get; set; } = false;
    public string? Value { get; set; }
}

public class CreateItemRequest : DynamicColumnRecord
{
    public ulong BoardId { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string GroupId { get; set; } = String.Empty;
}

public class CreateItemResponse
{ 
    public ulong Id { get; set; }
}

public class CreateSubItemRequest
{
    public ulong ItemId { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
}

public class CreateSubItemResponse
{ 
}