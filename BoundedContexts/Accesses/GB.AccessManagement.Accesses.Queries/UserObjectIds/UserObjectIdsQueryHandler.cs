using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.UserObjectIds;

public sealed class ListUserObjectIdsQueryHandler : QueryHandler<UserObjectIdsQuery, string[]>
{
    private readonly IObjectIdProvider provider;

    public ListUserObjectIdsQueryHandler(IObjectIdProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task<string[]> Handle(UserObjectIdsQuery query)
    {
        var objectIds = await this.provider.List(query.UserId, query.ObjectType, query.Relation);

        return objectIds
            .Select(id => (string)id)
            .ToArray();
    }
}