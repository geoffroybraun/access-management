using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Queries;

public interface IUserAccessRepository
{
    Task<string?> GetRelation(string userId, string objectType, string objectId);

    Task<UserAccess[]> List(string userId, string objectType);
}