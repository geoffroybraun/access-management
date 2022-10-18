using System.Net;
using GB.AccessManagement.Accesses.Infrastructure;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class HttpClientConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services.AddOptions<OpenFgaOptions>()
            .BindConfiguration("OpenFga")
            .ValidateDataAnnotations()
            .Services
            .AddHttpClient("default")
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { AutomaticDecompression = DecompressionMethods.Deflate })
            .AddPolicyHandler((provider, _) => ConfigurePolicyHandlers(provider));
    }

    private static IAsyncPolicy<HttpResponseMessage> ConfigurePolicyHandlers(IServiceProvider provider)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(
                3,
                retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt)))
            .WrapAsync(Policy.TimeoutAsync(TimeSpan.FromMilliseconds(10000)));
    }
}