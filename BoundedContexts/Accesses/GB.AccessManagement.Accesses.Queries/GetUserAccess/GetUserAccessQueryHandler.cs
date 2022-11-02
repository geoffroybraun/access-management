using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.GetUserAccess;

public sealed class GetUserAccessQueryHandler : QueryHandler<GetUserAccessQuery, UserAccess?>
{
    private readonly IUserAccessRepository repository;

    public GetUserAccessQueryHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<UserAccess?> Handle(GetUserAccessQuery query)
    {
        string? relation = await this.repository.GetRelation(query.UserId, query.ObjectType, query.ObjectId);

        if (string.IsNullOrEmpty(relation))
        {
            return default;
        }

        return new(query.UserId, query.ObjectType, query.ObjectId, relation);
    }
}