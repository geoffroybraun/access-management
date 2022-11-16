using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Domain.Exceptions;
using GB.AccessManagement.Companies.Domain.Policies;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using MediatR;

namespace GB.AccessManagement.Companies.Infrastructure.Policies;

public sealed class CompanyOwnerPolicy : ICompanyOwnerPolicy, IScopedService
{
    private const string ObjectType = "companies";
    private const string Relation = "owner";
    private readonly IMediator mediator;

    public CompanyOwnerPolicy(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task EnsureUserIsCompanyOwner(UserId userId, CompanyId companyId)
    {
        var query = new CanAccessQuery(userId.ToString(), ObjectType, companyId.ToString(), Relation);

        if (!await this.mediator.Send(query))
        {
            throw new MissingCompanyAccessException(userId.ToString(), Relation, companyId.ToString());
        }
    }
}