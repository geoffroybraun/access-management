namespace GB.AccessManagement.Accesses.Contracts.Providers;

public interface IObjectIdProvider
{
    Task<string[]> List(string userId, string objectType, string relation);
}