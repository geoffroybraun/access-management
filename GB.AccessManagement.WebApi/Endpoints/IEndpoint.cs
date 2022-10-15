namespace GB.AccessManagement.WebApi.Endpoints;

public interface IEndpoint<in TRequest> where TRequest : notnull
{
    Task<IResult> Handle(TRequest request);
}