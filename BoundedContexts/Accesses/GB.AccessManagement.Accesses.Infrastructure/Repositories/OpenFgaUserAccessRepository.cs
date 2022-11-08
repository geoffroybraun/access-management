using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Accesses.Queries;
using GB.AccessManagement.Core.Services;
using GB.AccessManagement.Core.ValueTypes;
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
                    Object = access.Object,
                    Relation = access.Relation.ToString(),
                    User = access.UserId.ToString()
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
                    Object = access.Object,
                    Relation = access.Relation.ToString(),
                    User = access.UserId.ToString()
                }
            })
        });
    }

    async Task<string?> Queries.IUserAccessRepository.GetRelation(UserId userId, ObjectType objectType, ObjectId objectId)
    {
        using var api = this.CreateApi();
        var response = await api.Read(new ReadRequest
        {
            TupleKey = new()
            {
                Object = $"{objectType}:{objectId}",
                User = userId.ToString()
            }
        });

        return response.Tuples?.FirstOrDefault()?.Key?.Relation;
    }

    async Task<UserAccessPresentation[]> Queries.IUserAccessRepository.List(UserId userId, ObjectType objectType)
    {
        using var api = this.CreateApi();
        var response = await api.Read(new ReadRequest()
        {
            TupleKey = new()
            {
                User = userId.ToString(),
                Object = $"{objectType}:"
            }
        });

        return response.Tuples?.Select(tuple =>
            {
                string[] objectValues = tuple.Key?.Object?.Split(':')!;

                return new UserAccessPresentation(userId, objectValues.First(), objectValues.Last(), tuple.Key!.Relation!);
            })
            .ToArray() ?? Array.Empty<UserAccessPresentation>();
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