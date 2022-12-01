using GB.AccessManagement.Core.Exceptions;
using Hellang.Middleware.ProblemDetails;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class ProblemDetailsConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddProblemDetails(options =>
        {
            options.Map<DomainException>((context, exception) =>
            {
                var factory = context
                    .RequestServices
                    .GetRequiredService<ProblemDetailsFactory>();

                return factory.CreateProblemDetails(
                    context,
                    statusCode: StatusCodes.Status422UnprocessableEntity,
                    title: exception.Title,
                    type: exception.GetType().Name,
                    detail: exception.Message);
            });
            
            options.Map<Exception>((_, e) => throw e);
            
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });
    }
}