using GB.AccessManagement.Accesses.Domain.ValueTypes;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Visitors;

public interface IUserSetTreeVisitor
{
    Task<UserId[]?> Visit(UsersetTree? tree);

    Task<UserId[]?> Visit(Node? node);

    Task<UserId[]?> Visit(Nodes? nodes);

    Task<UserId[]?> Visit(Leaf? leaf);

    Task<UserId[]?> Visit(UsersetTreeDifference? difference);

    Task<UserId[]?> Visit(List<Computed>? computedList);
    
    Task<UserId[]?> Visit(Computed? computed);

    Task<UserId[]?> Visit(UsersetTreeTupleToUserset? userSet);

    Task<UserId[]?> Visit(Users? users);

    Task<UserId[]?> Visit(string? set);
}