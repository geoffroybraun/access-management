using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Domain.Providers;

public interface IUserIdProvider
{
    Task<UserId[]?> List(ObjectType objectType, ObjectId objectId, Relation relation);
}