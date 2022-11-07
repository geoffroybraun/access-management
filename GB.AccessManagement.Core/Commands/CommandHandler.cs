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

public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await this.Handle(request);
    }

    protected abstract Task<TResponse> Handle(TCommand command);
}