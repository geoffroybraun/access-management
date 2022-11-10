using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;

public static class NodeExtension
{
    public static async Task<UserId[]?> Accept(this List<Node> nodes, IUserSetTreeVisitor visitor)
    {
        List<UserId> result = new();

        foreach (var node in nodes)
        {
            result.AddRange(await visitor.Visit(node));
        }

        return result.ToArray();
    }
    
    public static async Task<UserId[]?> Accept(this Node node, IUserSetTreeVisitor visitor)
    {
        return Array.Empty<UserId>()
            .Union(await visitor.Visit(node.Difference))
            .Union(await visitor.Visit(node.Leaf))
            .Union(await visitor.Visit(node.Intersection))
            .Union(await visitor.Visit(node.Union))
            .ToArray();
    }
}