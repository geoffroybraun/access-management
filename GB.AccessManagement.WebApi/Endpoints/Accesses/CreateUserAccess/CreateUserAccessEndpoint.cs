using GB.AccessManagement.Accesses.Commands.CreateUserAccess;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.CreateUserAccess;

public sealed class CreateUserAccessEndpoint : IEndpoint<CreateUserAccessCommand>
{
    private readonly IMediator mediator;
    private readonly IHttpContextAccessor httpContextAccessor;

    public CreateUserAccessEndpoint(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        this.mediator = mediator;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IResult> Handle(CreateUserAccessCommand request)
    {
        await mediator.Send(request);
        string createdUri = $"{this.httpContextAccessor.HttpContext!.Request.Path}/{request.ObjectType}/{request.ObjectId}";

        return Results.Created(createdUri, request);
    }
}