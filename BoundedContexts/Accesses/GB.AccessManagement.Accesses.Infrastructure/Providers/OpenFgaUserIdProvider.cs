using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Providers;

public sealed class OpenFgaUserIdProvider : IUserIdProvider, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaUserIdProvider(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    public async Task<UserId[]?> List(ObjectType objectType, ObjectId objectId, Relation relation)
    {
        using var api = this.CreateApi();
        var response = await api.Read(new ReadRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType.ToString()}:{objectId.ToString()}",
                Relation = relation.ToString()
            }
        });

        return response.Tuples?.Select(tuple => (UserId)tuple.Key!.User!).ToArray() ?? Array.Empty<UserId>();
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