using GB.AccessManagement.Accesses.Commands;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
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

    async Task Commands.IUserAccessRepository.Create(UserAccess access)
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
                    User = access.UserId.ToString()
                }
            })
        });
    }

    async Task Commands.IUserAccessRepository.Delete(UserAccess access)
    {
        using var api = this.factory.CreateApi(this.options);
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
}