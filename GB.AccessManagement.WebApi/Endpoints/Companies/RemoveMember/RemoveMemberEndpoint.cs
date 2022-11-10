using GB.AccessManagement.Companies.Commands.RemoveMember;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.RemoveMember;

public sealed class RemoveMemberEndpoint : IEndpoint<RemoveMemberCommand>
{
    private readonly IMediator mediator;

    public RemoveMemberEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(RemoveMemberCommand request)
    {
        await this.mediator.Send(request);

        return Results.NoContent();
    }
}