namespace Catalog.Application.Common.Response.CustomResponses.Base;

public record BaseResponse
{
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
}