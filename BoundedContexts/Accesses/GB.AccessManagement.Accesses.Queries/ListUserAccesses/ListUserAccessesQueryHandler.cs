using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListUserAccesses;

public sealed class ListUserAccessesQueryHandler : QueryHandler<ListUserAccessesQuery, UserAccessPresentation[]>
{
    private readonly IUserAccessRepository repository;

    public ListUserAccessesQueryHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<UserAccessPresentation[]> Handle(ListUserAccessesQuery query)
    {
        return await this.repository.List(query.UserId, query.ObjectType);
    }
}