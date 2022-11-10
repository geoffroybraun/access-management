using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.AddMember;

public sealed class AddMemberCommandHandler : CommandHandler<AddMemberCommand>
{
    private readonly ICompanyRepository repository;

    public AddMemberCommandHandler(ICompanyRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task Handle(AddMemberCommand command)
    {
        var aggregate = await this.repository.Load(command.CompanyId);
        aggregate.AddMember(command.MemberId);
        await this.repository.Save(aggregate);
    }
}