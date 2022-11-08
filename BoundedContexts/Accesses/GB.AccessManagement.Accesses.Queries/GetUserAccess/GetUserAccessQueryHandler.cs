using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.GetUserAccess;

public sealed class GetUserAccessQueryHandler : QueryHandler<GetUserAccessQuery, UserAccessPresentation?>
{
    private readonly IUserAccessRepository repository;

    public GetUserAccessQueryHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<UserAccessPresentation?> Handle(GetUserAccessQuery query)
    {
        string? relation = await this.repository.GetRelation(query.UserId, query.ObjectType, query.ObjectId);

        if (string.IsNullOrEmpty(relation))
        {
            return default;
        }

        return new(query.UserId, query.ObjectType, query.ObjectId, relation);
    }
}