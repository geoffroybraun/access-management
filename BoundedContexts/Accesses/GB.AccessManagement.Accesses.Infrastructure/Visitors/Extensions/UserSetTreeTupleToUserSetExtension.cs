using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class UserSetTreeTupleToUserSetExtension
{
    public static async Task<UserId[]> Accept(this UsersetTreeTupleToUserset userSet, IUserSetTreeVisitor visitor)
    {
        return Array.Empty<UserId>()
            .Union(await visitor.Visit(userSet.Computed))
            .Union(await visitor.Visit(userSet.Tupleset))
            .ToArray();
    }
}