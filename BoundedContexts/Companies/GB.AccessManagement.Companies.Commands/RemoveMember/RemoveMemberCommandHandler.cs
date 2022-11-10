using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.RemoveMember;

public sealed class RemoveMemberCommandHandler : CommandHandler<RemoveMemberCommand>
{
    private readonly ICompanyRepository repository;

    public RemoveMemberCommandHandler(ICompanyRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task Handle(RemoveMemberCommand command)
    {
        var aggregate = await this.repository.Load(command.CompanyId);
        aggregate.RemoveMember(command.MemberId);
        await this.repository.Save(aggregate);
    }
}