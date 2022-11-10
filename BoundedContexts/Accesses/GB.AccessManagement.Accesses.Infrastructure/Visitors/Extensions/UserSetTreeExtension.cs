using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class UserSetTreeExtension
{
    public static async Task<UserId[]?> Accept(this UsersetTree tree, IUserSetTreeVisitor visitor)
    {
        return await visitor.Visit(tree.Root);
    }
}