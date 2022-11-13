using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Accesses.Contracts.Presentations;
using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.WebApi.Controllers.Requests.Accesses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Controllers;

[ApiController]
[Route("v1/users/{user}/accesses")]
[Authorize]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
public sealed class AccessController : ControllerBase
{
    private readonly IMediator mediator;

    public AccessController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateUserAccessRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromRoute(Name = "user")] Guid userId,
        [FromBody] CreateUserAccessRequest request)
    {
        _ = await this.mediator.Send(request.ToCommand(userId));
        string accessEndpoint = $"/users/{userId}/accesses/{request.ObjectType}/{request.ObjectId}";

        return this.Created(accessEndpoint, request);
    }

    [HttpGet("{type}")]
    [ProducesResponseType(typeof(UserAccessPresentation[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> List(
        [FromRoute(Name = "user")] Guid userId,
        [FromRoute(Name = "type")] string objectType)
    {
        ListUserAccessesQuery query = new(userId, objectType);
        var presentations = await this.mediator.Send(query);

        return this.Ok(presentations);
    }

    [HttpGet("{type}/{object}")]
    [ProducesResponseType(typeof(UserAccessPresentation), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Single(
        [FromRoute(Name = "user")] Guid userId,
        [FromRoute(Name = "type")] string objectType,
        [FromRoute(Name = "object")] string objectId)
    {
        GetUserAccessQuery query = new(userId, objectType, objectId);
        var presentation = await this.mediator.Send(query);

        return presentation is not null ? this.Ok(presentation) : this.NotFound();
    }

    [HttpDelete("{type}/{object}/{relation}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete(
        [FromRoute(Name = "user")] Guid userId,
        [FromRoute(Name = "type")] string objectType,
        [FromRoute(Name = "object")] string objectId,
        [FromRoute] string relation)
    {
        DeleteUserAccessCommand command = new(userId, objectType, objectId, relation);
        _ = await this.mediator.Send(command);

        return this.NoContent();
    }
}