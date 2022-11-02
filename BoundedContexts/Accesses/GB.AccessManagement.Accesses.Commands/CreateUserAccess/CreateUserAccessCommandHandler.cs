using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Commands.CreateUserAccess;

public sealed class CreateUserAccessCommandHandler : CommandHandler<CreateUserAccessCommand>
{
    private readonly IUserAccessRepository repository;

    public CreateUserAccessCommandHandler(IUserAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task Handle(CreateUserAccessCommand command)
    {
        var access = new UserAccess(command.UserId, command.ObjectType, command.ObjectId, command.Relation);
        await this.repository.Create(access);
    }
}