using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListObjectUserIds;

public sealed class ListObjectUserIdsQueryHandler : QueryHandler<ListObjectUserIdsQuery, string[]>
{
    private readonly IUserIdProvider provider;
    private readonly IRecursiveUserIdProvider recursiveProvider;

    public ListObjectUserIdsQueryHandler(IUserIdProvider provider, IRecursiveUserIdProvider recursiveProvider)
    {
        this.provider = provider;
        this.recursiveProvider = recursiveProvider;
    }

    protected override async Task<string[]> Handle(ListObjectUserIdsQuery query)
    {
        UserId[] userIds;

        switch (query.IsRecursive)
        {
            case true:
                userIds = await this.recursiveProvider.Expand(query.ObjectType, query.ObjectId, query.Relation);
                break;
            
            case false:
                userIds = await this.provider.List(query.ObjectType, query.ObjectId, query.Relation);
                break;
        }

        return userIds
            .Select(id => (string)id)
            .ToArray();
    }
}