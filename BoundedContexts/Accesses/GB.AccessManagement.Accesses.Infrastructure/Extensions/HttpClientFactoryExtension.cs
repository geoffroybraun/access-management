using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;

namespace GB.AccessManagement.Accesses.Infrastructure.Extensions;

internal static class HttpClientFactoryExtension
{
    public static OpenFgaApi CreateApi(this IHttpClientFactory factory, OpenFgaOptions options)
    {
        var configuration = new Configuration()
        {
            ApiHost = options.Host,
            ApiScheme = options.Scheme
        };

        if (options.IsValid())
        {
            configuration.StoreId = options.StoreId;
        }
        
        return new OpenFgaApi(configuration, factory.CreateClient("default"));
    }
}