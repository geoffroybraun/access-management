namespace GB.AccessManagement.Accesses.Domain.ValueTypes;

public sealed record ObjectAccess(ObjectType ParentType, ObjectId ParentId, ObjectType ObjectType, ObjectId ObjectId, Relation Relation)
{
    public string Parent => $"{this.ParentType}:{this.ParentId}";

    public string Object => $"{this.ObjectType}:{this.ObjectId}";
}