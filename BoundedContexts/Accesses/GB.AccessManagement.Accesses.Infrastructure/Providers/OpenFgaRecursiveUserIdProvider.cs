using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Visitors;
using GB.AccessManagement.Core.Services;
using GB.AccessManagement.Core.ValueTypes;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Providers;

public sealed class OpenFgaRecursiveUserIdProvider : IRecursiveUserIdProvider, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;
    private readonly IUserSetTreeVisitor visitor;

    public OpenFgaRecursiveUserIdProvider(
        IOptions<OpenFgaOptions> options,
        IHttpClientFactory factory,
        IUserSetTreeVisitor visitor)
    {
        this.factory = factory;
        this.visitor = visitor;
        this.options = options.Value;
    }

    public async Task<UserId[]> Expand(ObjectType objectType, ObjectId objectId, Relation relation)
    {
        using var api = this.CreateApi();
        var response = await api.Expand(new ExpandRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType.ToString()}:{objectId.ToString()}",
                Relation = relation.ToString()
            }
        });

        return await this.visitor.Visit(response.Tree);

        return response
                   .Tree?
                   .Root?
                   .Union?
                   ._Nodes?
                   .SelectMany(node => node.Leaf?.Users?._Users?.Select(user => (UserId)user) ?? Array.Empty<UserId>())
                   .ToArray()
               ?? Array.Empty<UserId>();
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