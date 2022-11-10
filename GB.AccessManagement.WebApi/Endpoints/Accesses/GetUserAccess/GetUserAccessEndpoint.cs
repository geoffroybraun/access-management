using GB.AccessManagement.Accesses.Contracts.Queries;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.GetUserAccess;

public sealed class GetUserAccessEndpoint : IEndpoint<GetUserAccessQuery>
{
    private readonly IMediator mediator;

    public GetUserAccessEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(GetUserAccessQuery request)
    {
        var access = await this.mediator.Send(request);

        if (access is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(access);
    }
}