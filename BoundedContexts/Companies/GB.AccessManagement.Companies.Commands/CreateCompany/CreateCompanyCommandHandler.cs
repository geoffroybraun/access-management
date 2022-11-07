using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.CreateCompany;

public sealed class CreateCompanyCommandHandler : CommandHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyRepository repository;

    public CreateCompanyCommandHandler(ICompanyRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task<Guid> Handle(CreateCompanyCommand command)
    {
        var aggregate = CompanyAggregate.Create(command.Name);
        aggregate.DefineOwnerId(command.OwnerId);
        await this.repository.Save(aggregate);

        return aggregate.Id;
    }
}