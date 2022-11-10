using GB.AccessManagement.Companies.Contracts.Commands;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.AddMember;

public sealed class AddMemberEndpoint : IEndpoint<AddMemberCommand>
{
    private readonly IMediator mediator;

    public AddMemberEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(AddMemberCommand request)
    {
        await this.mediator.Send(request);

        return Results.Accepted();
    }
}