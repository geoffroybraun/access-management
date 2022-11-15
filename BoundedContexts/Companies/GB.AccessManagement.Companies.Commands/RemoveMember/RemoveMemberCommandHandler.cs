using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.RemoveMember;

public sealed class RemoveMemberCommandHandler : CommandHandler<RemoveMemberCommand>
{
    private readonly ICompanyStore _store;

    public RemoveMemberCommandHandler(ICompanyStore store)
    {
        this._store = store;
    }

    protected override async Task Handle(RemoveMemberCommand command)
    {
        var aggregate = await this._store.Load(command.CompanyId);
        aggregate.RemoveMember(command.MemberId);
        await this._store.Save(aggregate);
    }
}