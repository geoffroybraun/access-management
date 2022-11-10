using GB.AccessManagement.Accesses.Contracts.Commands;

namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record UserAccess(UserId UserId, ObjectType ObjectType, ObjectId ObjectId, Relation Relation)
{
    public string Object => $"{this.ObjectType}:{this.ObjectId}";
    
    public static implicit operator UserAccess(DeleteUserAccessCommand command)
    {
        return new(command.UserId, command.ObjectType, command.ObjectId, command.Relation);
    }
}