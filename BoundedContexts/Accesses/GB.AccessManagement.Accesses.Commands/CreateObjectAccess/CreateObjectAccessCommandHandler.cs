using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Accesses.Commands.CreateObjectAccess;

public sealed class CreateObjectAccessCommandHandler : CommandHandler<CreateObjectAccessCommand>
{
    private readonly IObjectAccessRepository repository;

    public CreateObjectAccessCommandHandler(IObjectAccessRepository repository)
    {
        this.repository = repository;
    }

    protected override async Task Handle(CreateObjectAccessCommand command)
    {
        ObjectAccess access = new(command.ParentType, command.ParentId, command.ObjectType, command.ObjectId, command.Relation);
        await this.repository.Create(access);
    }
}