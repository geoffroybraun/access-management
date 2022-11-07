using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Commands.DeleteUserAccess;

public sealed record DeleteUserAccessCommand(string UserId, string ObjectType, string ObjectId, string Relation) : ICommand
{
    public static implicit operator UserAccess(DeleteUserAccessCommand command)
    {
        return new(command.UserId, command.ObjectType, command.ObjectId, command.Relation);
    }
}