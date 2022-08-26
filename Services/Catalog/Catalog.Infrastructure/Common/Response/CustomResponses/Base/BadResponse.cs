namespace Catalog.Application.Common.Response.CustomResponses.Base;

public record BadResponse : BaseResponse
{
    public IEnumerable<object>? Errors { get; set; }
}