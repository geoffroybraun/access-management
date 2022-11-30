using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.CreateCompany;

public sealed class CreateCompanyCommandHandler : CommandHandler<CreateCompanyCommand>
{
    private readonly ICompanyAggregateCreator creator;
    private readonly ICompanyStore store;

    public CreateCompanyCommandHandler(ICompanyAggregateCreator creator, ICompanyStore store)
    {
        this.creator = creator;
        this.store = store;
    }

    protected override async Task Handle(CreateCompanyCommand command)
    {
        var aggregate = await this.creator.Create(command.Name, command.OwnerId, command.ParentCompanyId);
        await this.store.Save(aggregate);
    }
}