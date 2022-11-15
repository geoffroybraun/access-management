using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.RemoveMember;

public sealed class RemoveMemberCommandHandler : CommandHandler<RemoveMemberCommand>
{
    private readonly ICompanyStore store;

    public RemoveMemberCommandHandler(ICompanyStore store)
    {
        this.store = store;
    }

    protected override async Task Handle(RemoveMemberCommand command)
    {
        var aggregate = await this.store.Load(command.CompanyId);
        aggregate.RemoveMember(command.MemberId);
        await this.store.Save(aggregate);
    }
}