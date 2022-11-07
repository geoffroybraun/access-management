using MediatR;

namespace GB.AccessManagement.Core.Commands;

public interface ICommand : IRequest { }

public interface ICommand<out TResponse> : IRequest<TResponse> { }