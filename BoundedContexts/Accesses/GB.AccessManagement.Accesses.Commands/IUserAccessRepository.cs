using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Commands;

public interface IUserAccessRepository
{
    Task Create(UserAccess access);

    Task Delete(UserAccess access);
}