using Hellang.Middleware.ProblemDetails;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class ProblemDetailsConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services.AddProblemDetails(options =>
        {
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });
    }
}