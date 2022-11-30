using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
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

    public async Task<UserId[]> List(ObjectType objectType, ObjectId objectId, Relation relation)
    {
        using var api = this.factory.CreateApi(this.options);
        var response = await api.Read(new ReadRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType}:{objectId}",
                Relation = relation.ToString()
            }
        });

        return response
            .Tuples?
            .Select(tuple => (UserId)tuple.Key!.User!)
            .ToArray() ?? Array.Empty<UserId>();
    }
}