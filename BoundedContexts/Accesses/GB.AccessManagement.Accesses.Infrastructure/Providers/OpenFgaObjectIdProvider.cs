using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Providers;

public sealed class OpenFgaObjectIdProvider : IObjectIdProvider, IScopedService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaObjectIdProvider(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    public async Task<ObjectId[]> List(UserId userId, ObjectType objectType, Relation relation)
    {
        using var api = this.factory.CreateApi(this.options);
        var response = await api.ListObjects(new ListObjectsRequest
        {
            User = userId.ToString(),
            Type = objectType.ToString(),
            Relation = relation.ToString()
        });

        return response?.ObjectIds?.Select(id => (ObjectId)id).ToArray() ?? Array.Empty<ObjectId>();
    }
}