using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.CreateCompany;

public sealed class CreateCompanyCommandHandler : CommandHandler<CreateCompanyCommand, Guid>
{
    private readonly ICompanyAggregateCreator creator;
    private readonly ICompanyRepository repository;

    public CreateCompanyCommandHandler(ICompanyAggregateCreator creator, ICompanyRepository repository)
    {
        this.creator = creator;
        this.repository = repository;
    }

    protected override async Task<Guid> Handle(CreateCompanyCommand command)
    {
        var aggregate = this.creator.Create(command.Name, command.OwnerId, command.ParentCompanyId);
        await this.repository.Save(aggregate);

        return aggregate.Id;
    }
}