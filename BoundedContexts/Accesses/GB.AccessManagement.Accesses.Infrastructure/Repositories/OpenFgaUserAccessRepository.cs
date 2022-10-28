using GB.AccessManagement.Accesses.Application;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Repositories;

public sealed class OpenFgaUserAccessRepository : IUserAccessRepository, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaUserAccessRepository(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    public async Task Create(UserAccess access)
    {
        var configuration = new Configuration()
        {
            ApiHost = options.Host,
            ApiScheme = options.Scheme,
            StoreId = options.StoreId
        };
        using var client = this.factory.CreateClient("default");
        using var api = new OpenFgaApi(configuration, client);
        _ = await api.Write(new WriteRequest
        {
            Writes = new TupleKeys(new List<TupleKey>()
            {
                new TupleKey
                {
                    Object = $"{access.ObjectType}:{access.ObjectId}",
                    Relation = access.Relation,
                    User = access.UserId
                }
            })
        });
    }
}