namespace GB.AccessManagement.Accesses.Queries;

public interface IUserAccessRepository
{
    Task<string?> GetRelation(string userId, string objectType, string objectId);
}