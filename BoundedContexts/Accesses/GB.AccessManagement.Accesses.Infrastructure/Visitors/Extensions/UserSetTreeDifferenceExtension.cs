using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class UserSetTreeDifferenceExtension
{
    public static async Task<UserId[]> Accept(this UsersetTreeDifference difference, IUserSetTreeVisitor visitor)
    {
        return Array.Empty<UserId>()
            .Union(await visitor.Visit(difference.Base))
            .Union(await visitor.Visit(difference.Subtract))
            .ToArray();
    }
}