using Hellang.Middleware.ProblemDetails;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class ProblemDetailsConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddProblemDetails(options =>
        {
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });
    }
}