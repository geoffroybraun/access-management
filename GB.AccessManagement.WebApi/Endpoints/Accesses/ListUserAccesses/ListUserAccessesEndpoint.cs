using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Queries.ListUserAccesses;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.ListUserAccesses;

public sealed class ListUserAccessesEndpoint : IEndpoint<ListUserAccessesQuery>
{
    private readonly IMediator mediator;

    public ListUserAccessesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(ListUserAccessesQuery request)
    {
        UserAccess[] accesses = await this.mediator.Send(request);

        return Results.Ok(accesses);
    }
}