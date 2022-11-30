using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Accesses.Infrastructure.Visitors;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
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
        using var api = this.factory.CreateApi(this.options);
        var response = await api.Expand(new ExpandRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType}:{objectId}",
                Relation = relation.ToString()
            }
        });

        return await this.visitor.Visit(response.Tree);
    }
}