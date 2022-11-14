using System.Text.RegularExpressions;
using GB.AccessManagement.Accesses.Domain.Providers;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Visitors.Extensions;
using GB.AccessManagement.Core.Services;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors;

public sealed class UserSetTreeVisitor : IUserSetTreeVisitor, IScopedService
{
    private const string ObjectTypeGroupName = "objectType";
    private const string ObjectIdGroupName = "objectId";
    private const string RelationGroupName = "relation";
    private static readonly UserId[] DefaultArray = Array.Empty<UserId>();
    private static readonly Regex SetRegex = new Regex($"(?<{ObjectTypeGroupName}>[^:]+):(?<{ObjectIdGroupName}>[^#]+)#(?<{RelationGroupName}>.+)", RegexOptions.Compiled);
    private readonly IUserIdProvider provider;

    public UserSetTreeVisitor(IUserIdProvider provider)
    {
        this.provider = provider;
    }

    public async Task<UserId[]> Visit(UsersetTree? tree)
    {
        if (tree is null)
        {
            return DefaultArray;
        }

        return await tree.Accept(this);
    }

    public async Task<UserId[]> Visit(Node? node)
    {
        if (node is null)
        {
            return DefaultArray;
        }

        return await node.Accept(this);
    }

    public async Task<UserId[]> Visit(Nodes? nodes)
    {
        if (nodes is null || nodes._Nodes is null || !nodes._Nodes.Any())
        {
            return DefaultArray;
        }

        return await nodes._Nodes.Accept(this);
    }

    public async Task<UserId[]> Visit(Leaf? leaf)
    {
        if (leaf is null)
        {
            return DefaultArray;
        }

        return await leaf.Accept(this);
    }

    public async Task<UserId[]> Visit(UsersetTreeDifference? difference)
    {
        if (difference is null)
        {
            return DefaultArray;
        }

        return await difference.Accept(this);
    }

    public async Task<UserId[]> Visit(List<Computed>? computedList)
    {
        if (computedList is null || !computedList.Any())
        {
            return DefaultArray;
        }

        return await computedList.Accept(this);
    }

    public async Task<UserId[]> Visit(Computed? computed)
    {
        if (computed is null)
        {
            return DefaultArray;
        }

        return await computed.Accept(this);
    }

    public async Task<UserId[]> Visit(UsersetTreeTupleToUserset? userSet)
    {
        if (userSet is null)
        {
            return DefaultArray;
        }

        return await userSet.Accept(this);
    }

    public Task<UserId[]> Visit(Users? users)
    {
        if (users is null || users._Users is null || !users._Users.Any())
        {
            return Task.FromResult(DefaultArray);
        }

        var result = users
            ._Users!
            .Select(user => (UserId)user)
            .ToArray();

        return Task.FromResult(result);
    }

    public async Task<UserId[]> Visit(string? set)
    {
        set = set?.Trim();

        if (string.IsNullOrEmpty(set) || string.IsNullOrWhiteSpace(set))
        {
            return DefaultArray;
        }

        var match = SetRegex.Match(set);

        if (!match.Success)
        {
            return DefaultArray;
        }

        string objectType = match.Groups[ObjectTypeGroupName].Value;
        string objectId = match.Groups[ObjectIdGroupName].Value;
        string relation = match.Groups[RelationGroupName].Value;

        return await this.provider.List(objectType, objectId, relation) ?? DefaultArray;
    }
}