using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Application;

public interface IUserAccessRepository
{
    Task Create(UserAccess access);

    Task Delete(UserAccess access);
}