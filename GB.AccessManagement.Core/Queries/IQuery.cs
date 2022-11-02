using MediatR;

namespace GB.AccessManagement.Core.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse> { }