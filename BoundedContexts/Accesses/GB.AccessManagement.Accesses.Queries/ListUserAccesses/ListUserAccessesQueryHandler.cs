using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListUserAccesses;

public sealed class ListUserAccessesQueryHandler : QueryHandler<ListUserAccessesQuery, UserAccess[]>
{
    private readonly IUserAccessRepository repository;

    public ListUserAccessesQueryHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<UserAccess[]> Handle(ListUserAccessesQuery query)
    {
        return await this.repository.List(query.UserId, query.ObjectType);
    }
}