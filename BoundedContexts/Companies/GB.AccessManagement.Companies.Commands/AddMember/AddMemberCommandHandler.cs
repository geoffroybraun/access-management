using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.AddMember;

public sealed class AddMemberCommandHandler : CommandHandler<AddMemberCommand>
{
    private readonly ICompanyStore _store;

    public AddMemberCommandHandler(ICompanyStore store)
    {
        this._store = store;
    }

    protected override async Task Handle(AddMemberCommand command)
    {
        var aggregate = await this._store.Load(command.CompanyId);
        aggregate.AddMember(command.MemberId);
        await this._store.Save(aggregate);
    }
}