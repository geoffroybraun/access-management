using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Accesses.Domain.Evaluators;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.CanAccess;

public sealed class CanAccessQueryHandler : QueryHandler<CanAccessQuery, bool>
{
    private readonly IUserAccessEvaluator evaluator;

    public CanAccessQueryHandler(IUserAccessEvaluator evaluator)
    {
        this.evaluator = evaluator;
    }

    protected override async Task<bool> Handle(CanAccessQuery query)
    {
        var access = new UserAccess(query.UserId, query.ObjectType, query.ObjectId, query.Relation);

        return await this.evaluator.CanAccess(access);
    }
}