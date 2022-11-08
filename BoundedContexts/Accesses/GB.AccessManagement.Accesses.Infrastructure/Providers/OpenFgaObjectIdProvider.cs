using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.Services;
using GB.AccessManagement.Core.ValueTypes;
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

    public async Task<ObjectId[]> List(UserId userId, ObjectType objectType, Relation relation)
    {
        using var api = this.CreateApi();
        var response = await api.ListObjects(new ListObjectsRequest
        {
            User = userId.ToString(),
            Type = objectType.ToString(),
            Relation = relation.ToString()
        });

        return response?.ObjectIds?.Select(id => (ObjectId)id).ToArray() ?? Array.Empty<ObjectId>();
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