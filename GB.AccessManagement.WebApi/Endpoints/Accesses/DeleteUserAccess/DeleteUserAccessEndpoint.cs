using GB.AccessManagement.Accesses.Contracts.Commands;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.DeleteUserAccess;

public sealed class DeleteUserAccessEndpoint : IEndpoint<DeleteUserAccessCommand>
{
    private readonly IMediator mediator;

    public DeleteUserAccessEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(DeleteUserAccessCommand request)
    {
        await this.mediator.Send(request);

        return Results.NoContent();
    }
}