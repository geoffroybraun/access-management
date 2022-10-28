using Hellang.Middleware.ProblemDetails;

namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class ProblemDetailsConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app.UseProblemDetails();
    }
}