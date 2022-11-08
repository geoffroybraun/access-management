using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Accesses.Contracts.ValueTypes;

public sealed record UserAccess(UserId UserId, ObjectType ObjectType, ObjectId ObjectId, Relation Relation)
{
    public string Object => $"{this.ObjectType}:{this.ObjectId}";
}