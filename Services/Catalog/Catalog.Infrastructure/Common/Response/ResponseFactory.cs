namespace Catalog.Application.Common.Response;

public record ResponseFactory : IResponseFactory
{
    public ConflictResponse ConflictResponse(string message, object? data = null, IEnumerable<dynamic>? errors = null)
    {
        return new ConflictResponse
        {
            Success = false,
            Data = data,
            Message = message,
            Errors = errors,
            StatusCode = HttpStatusCode.Conflict
        };
    }
    
    public NotFoundResponse NotFoundResponse(string message, object? data = null, IEnumerable<dynamic>? errors = null)
    {
        return new NotFoundResponse
        {
            Success = false,
            Data = data,
            Message = message,
            Errors = errors,
            StatusCode = HttpStatusCode.NotFound
        };
    }
    
    public SuccessResponse SuccessResponse(object data, string? message = null)
    {
        return new SuccessResponse
        {
            Success = true,
            Data = data,
            Message = message,
            StatusCode = HttpStatusCode.OK
        };
    }
}