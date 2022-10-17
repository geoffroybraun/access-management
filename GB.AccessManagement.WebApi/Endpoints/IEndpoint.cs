using GB.AccessManagement.WebApi.Services;

namespace GB.AccessManagement.WebApi.Endpoints;

public interface IEndpoint<in TRequest> : IScopedService where TRequest : notnull
{
    Task<IResult> Handle(TRequest request);
}