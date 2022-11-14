using GB.AccessManagement.Accesses.Commands;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Repositories;

public sealed class OpenFgaObjectAccessRepository : IObjectAccessRepository, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaObjectAccessRepository(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.options = options.Value;
        this.factory = factory;
    }

    public async Task Create(ObjectAccess access)
    {
        using var api = this.CreateApi();
        _ = await api.Write(new WriteRequest
        {
            Writes = new TupleKeys(new List<TupleKey>
            {
                new()
                {
                    Object = access.Object,
                    Relation = access.Relation.ToString(),
                    User = access.Parent
                }
            })
        });
    }

    private OpenFgaApi CreateApi()
    {
        var configuration = new Configuration
        {
            ApiHost = this.options.Host,
            ApiScheme = this.options.Scheme,
            StoreId = this.options.StoreId
        };
        
        return new(configuration, this.factory.CreateClient("default"));
    }
}