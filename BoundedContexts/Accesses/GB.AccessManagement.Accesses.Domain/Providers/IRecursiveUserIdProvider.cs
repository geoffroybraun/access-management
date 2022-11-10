using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Domain.Providers;

public interface IRecursiveUserIdProvider
{
    Task<UserId[]?> Expand(ObjectType objectType, ObjectId objectId, Relation relation);
}