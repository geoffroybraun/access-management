namespace GB.AccessManagement.WebApi.Configurations;

public interface IMiddlewareConfiguration
{
    void Use(WebApplication app);
}