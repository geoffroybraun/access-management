using GB.AccessManagement.Companies.Domain.Exceptions;
using GB.AccessManagement.Companies.Domain.Policies;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Companies.Queries;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Companies.Infrastructure.Policies;

public sealed class CompanyExistPolicy : ICompanyExistPolicy, IScopedService
{
    private readonly ICompanyRepository repository;

    public CompanyExistPolicy(ICompanyRepository repository)
    {
        this.repository = repository;
    }

    public async Task EnsureCompanyExists(CompanyId id)
    {
        if (!await this.repository.Exist(id))
        {
            throw new NonExistentCompanyException(id);
        }
    }
}