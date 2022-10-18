namespace GB.AccessManagement.WebApi.Extensions;

public static partial class WebApplicationExtension
{
    public static WebApplication UseAuthentication(this WebApplication app)
    {
        _ = ((IApplicationBuilder)app).UseAuthentication();

        return app;
    }

    public static WebApplication UseAuthorization(this WebApplication app)
    {
        _ = ((IApplicationBuilder)app).UseAuthorization();

        return app;
    }
}