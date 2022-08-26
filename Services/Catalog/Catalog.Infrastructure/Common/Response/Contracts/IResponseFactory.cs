using Catalog.Application.Common.Response.CustomResponses;

namespace Catalog.Application.Common.Response.Contracts;

public interface IResponseFactory
{
    ConflictResponse ConflictResponse(string message, object? data = null, IEnumerable<dynamic>? errors = null);
    NotFoundResponse NotFoundResponse(string message, object? data = null, IEnumerable<dynamic>? errors = null);
    SuccessResponse SuccessResponse(object data, string? message = null);
}