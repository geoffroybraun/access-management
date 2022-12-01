namespace GB.AccessManagement.WebApi.Configurations;

public interface IMiddlewareConfiguration
{
    void Use(IApplicationBuilder app);
}