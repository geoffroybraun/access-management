using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class LeafExtension
{
    public static async Task<UserId[]?> Accept(this Leaf leaf, IUserSetTreeVisitor visitor)
    {
        return Array.Empty<UserId>()
            .Union(await visitor.Visit(leaf.Computed))
            .Union(await visitor.Visit(leaf.TupleToUserset))
            .Union(await visitor.Visit(leaf.Users))
            .ToArray();
    }
}