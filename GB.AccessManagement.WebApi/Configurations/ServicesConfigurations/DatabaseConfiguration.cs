using Microsoft.EntityFrameworkCore;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class DatabaseConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddScoped(_ =>
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("access-management");

            return builder.Options;
        });
    }
}