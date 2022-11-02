using MediatR;

namespace GB.AccessManagement.Core.Queries;

public abstract class QueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    public async Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return await this.Handle(request);
    }

    protected abstract Task<TResponse> Handle(TQuery query);
}