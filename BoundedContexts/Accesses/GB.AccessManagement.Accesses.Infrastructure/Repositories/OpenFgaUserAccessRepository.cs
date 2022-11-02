using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Repositories;

public sealed class OpenFgaUserAccessRepository : Commands.IUserAccessRepository,
    Queries.IUserAccessRepository,
    IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaUserAccessRepository(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    async Task Commands.IUserAccessRepository.Create(UserAccess access)
    {
        using var api = this.CreateApi();
        _ = await api.Write(new WriteRequest
        {
            Writes = new TupleKeys(new List<TupleKey>
            {
                new()
                {
                    Object = $"{access.ObjectType}:{access.ObjectId}",
                    Relation = access.Relation,
                    User = access.UserId
                }
            })
        });
    }

    async Task Commands.IUserAccessRepository.Delete(UserAccess access)
    {
        using var api = this.CreateApi();
        _ = await api.Write(new WriteRequest
        {
            Deletes = new TupleKeys(new List<TupleKey>
            {
                new()
                {
                    Object = $"{access.ObjectType}:{access.ObjectId}",
                    Relation = access.Relation,
                    User = access.UserId
                }
            })
        });
    }

    async Task<string?> Queries.IUserAccessRepository.GetRelation(string userId, string objectType, string objectId)
    {
        using var api = this.CreateApi();
        var response = await api.Read(new ReadRequest()
        {
            TupleKey = new()
            {
                Object = $"{objectType}:{objectId}",
                User = userId
            }
        });

        return response.Tuples?.FirstOrDefault()?.Key?.Relation;
    }

    private OpenFgaApi CreateApi()
    {
        var configuration = new Configuration()
        {
            ApiHost = options.Host,
            ApiScheme = options.Scheme,
            StoreId = options.StoreId
        };
        
        return new OpenFgaApi(configuration, this.factory.CreateClient("default"));
    }
}