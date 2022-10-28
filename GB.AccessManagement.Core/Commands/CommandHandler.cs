using MediatR;

namespace GB.AccessManagement.Core.Commands;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        await this.Handle(request);

        return Unit.Value;
    }

    protected abstract Task Handle(TCommand command);
}