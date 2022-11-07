using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Providers;

public sealed class OpenFgaObjectIdProvider : IObjectIdProvider, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaObjectIdProvider(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    public async Task<string[]> List(string userId, string objectType, string relation)
    {
        using var api = this.CreateApi();
        var response = await api.ListObjects(new ListObjectsRequest
        {
            User = userId,
            Type = objectType,
            Relation = relation
        });

        return response?.ObjectIds?.ToArray() ?? Array.Empty<string>();
    }

    private OpenFgaApi CreateApi()
    {
        var configuration = new Configuration
        {
            ApiHost = this.options.Host,
            ApiScheme = this.options.Scheme,
            StoreId = this.options.StoreId
        };

        return new OpenFgaApi(configuration, this.factory.CreateClient("default"));
    }
}