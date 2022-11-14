using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Commands;

public interface IObjectAccessRepository
{
    Task Create(ObjectAccess access);
}