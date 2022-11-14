using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListObjectObjectIds;

public sealed class ListObjectObjectIdsQueryHandler : QueryHandler<ListObjectObjectIdsQuery, string[]>
{
    private readonly IObjectAccessRepository repository;

    public ListObjectObjectIdsQueryHandler(IObjectAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<string[]> Handle(ListObjectObjectIdsQuery query)
    {
        return await this.repository.List(query.ObjectType, query.ObjectId, query.Relation);
    }
}