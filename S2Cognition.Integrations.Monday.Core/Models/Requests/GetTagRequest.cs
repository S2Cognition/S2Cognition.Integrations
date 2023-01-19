﻿using S2Cognition.Integrations.Monday.Core.Models.Options;

namespace S2Cognition.Integrations.Monday.Core.Models.Requests;

internal interface IGetTagRequest : IMondayRequest
{
    ulong TagId { get; set; }

    ITagOptions TagOptions { get; set; }
}

internal interface IGetTagResult : IMondayResult
{
    Tag? Data { get; }
}
internal class GetTagResult : MondayResult, IGetTagResult
{
    public Tag? Data { get; set; }

    internal GetTagResult(Tag? data)
    {
        Data = data;
    }
}

internal class GetTagRequest : MondayRequest, IGetTagRequest
{
    public ulong TagId { get; set; }

    public ITagOptions TagOptions { get; set; }

    internal GetTagRequest(ulong tagId)
    {
        TagId = tagId;

        TagOptions = new TagOptions(RequestMode.Default);
    }

    internal GetTagRequest(ulong tagId, RequestMode mode)
        : this(tagId)
    {
        TagOptions = new TagOptions(mode);
    }
}
