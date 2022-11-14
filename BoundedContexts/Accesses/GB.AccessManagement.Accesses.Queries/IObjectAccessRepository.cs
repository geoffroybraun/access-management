using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Queries;

public interface IObjectAccessRepository
{
    Task<string[]> List(ObjectType objectType, ObjectId objectId, Relation relation);
}