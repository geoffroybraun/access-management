using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Commands.DeleteUserAccess;

public sealed class DeleteUserAccessCommandHandler : CommandHandler<DeleteUserAccessCommand>
{
    private readonly IUserAccessRepository repository;

    public DeleteUserAccessCommandHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task Handle(DeleteUserAccessCommand command)
    {
        await this.repository.Delete(command);
    }
}