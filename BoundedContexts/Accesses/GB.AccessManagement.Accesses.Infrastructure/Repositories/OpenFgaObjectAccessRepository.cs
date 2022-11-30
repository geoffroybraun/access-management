using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Repositories;

public sealed class OpenFgaObjectAccessRepository : Commands.IObjectAccessRepository, Queries.IObjectAccessRepository, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaObjectAccessRepository(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.options = options.Value;
        this.factory = factory;
    }

    async Task Commands.IObjectAccessRepository.Create(ObjectAccess access)
    {
        using var api = this.factory.CreateApi(this.options);
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

    async Task<string[]> Queries.IObjectAccessRepository.List(ObjectType objectType, ObjectId objectId, Relation relation)
    {
        using var api = this.factory.CreateApi(this.options);
        var response = await api.Read(new ReadRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType}:{objectId}",
                Relation = relation
            }
        });

        return response
                   .Tuples?
                   .Where(tuple => tuple.Key!.User!.Contains(':'))
                   .Select(tuple => tuple.Key!.User!)
                   .ToArray()
               ?? Array.Empty<string>();
    }
}