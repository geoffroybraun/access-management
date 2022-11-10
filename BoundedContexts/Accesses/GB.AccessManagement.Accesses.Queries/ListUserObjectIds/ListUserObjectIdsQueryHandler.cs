using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListUserObjectIds;

public sealed class ListUserObjectIdsQueryHandler : QueryHandler<ListUserObjectIdsQuery, string[]>
{
    private readonly IObjectIdProvider provider;

    public ListUserObjectIdsQueryHandler(IObjectIdProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task<string[]> Handle(ListUserObjectIdsQuery query)
    {
        var objectIds = await this.provider.List(query.UserId, query.ObjectType, query.Relation);

        return objectIds
            .Select(id => (string)id)
            .ToArray();
    }
}