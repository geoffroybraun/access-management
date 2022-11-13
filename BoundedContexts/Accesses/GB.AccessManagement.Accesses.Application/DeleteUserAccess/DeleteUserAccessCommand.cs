using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Application.DeleteUserAccess;

public sealed record DeleteUserAccessCommand(Guid UserId, string ObjectType, string ObjectId, string Relation) : ICommand
{
    public static implicit operator UserAccess(DeleteUserAccessCommand command)
    {
        return new(command.UserId.ToString(), command.ObjectType, command.ObjectId, command.Relation);
    }
}