using GB.AccessManagement.Companies.Contracts.Commands;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.WebApi.Controllers.Attributes;
using GB.AccessManagement.WebApi.Controllers.Requests.Companies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Controllers;

[ApiController]
[Route("v{version:apiVersion}"), Authorize]
[ApiVersion("1")]
[ConsumesJson, ProducesJson]
[ProblemResponseType(StatusCodes.Status401Unauthorized)]
[ProblemResponseType(StatusCodes.Status403Forbidden)]
[ProblemResponseType(StatusCodes.Status500InternalServerError)]
public sealed class CompanyController : ControllerBase
{
    private readonly IMediator mediator;

    public CompanyController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("users/{user}/companies")]
    [ProducesResponseType(typeof(CreateCompanyCommand), StatusCodes.Status201Created)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create(
        [FromRoute(Name = "user")] Guid userId,
        [FromBody] CreateCompanyRequest request)
    {
        CreateCompanyCommand command = new(request.Name, userId, request.ParentCompanyId);
        _ = await this.mediator.Send(command);

        return this.Created($"v1/users/{userId}/companies", command);
    }

    [HttpGet("users/{user}/companies")]
    [ProducesResponseType(typeof(CompanyPresentation[]), StatusCodes.Status200OK)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> List([FromRoute(Name = "user")] Guid userId)
    {
        UserCompaniesQuery query = new(userId);
        var companies = await this.mediator.Send(query);

        return this.Ok(companies);
    }

    [HttpGet("companies/{company}/owner")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Owner([FromRoute(Name = "company")] Guid companyId)
    {
        CompanyOwnerQuery query = new(companyId);
        var owner = await this.mediator.Send(query);

        return this.Ok(owner);
    }

    [HttpPost("companies/{company}/members")]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddMember(
        [FromRoute(Name = "company")] Guid companyId,
        [FromBody] AddMemberRequest request)
    {
        AddMemberCommand command = new(companyId, request.MemberId);
        _ = await this.mediator.Send(command);

        return this.Created($"/v1/companies/{companyId}/members", command);
    }

    [HttpGet("companies/{company}/members")]
    [ProducesResponseType(typeof(Guid[]), StatusCodes.Status200OK)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Members([FromRoute(Name = "company")] Guid companyId)
    {
        CompanyMembersQuery query = new(companyId);
        var members = await this.mediator.Send(query);

        return this.Ok(members);
    }

    [HttpDelete("companies/{company}/members/{member}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RemoveMember(
        [FromRoute(Name = "company")] Guid companyId,
        [FromRoute(Name = "member")] Guid memberId)
    {
        RemoveMemberCommand command = new(companyId, memberId);
        _ = await this.mediator.Send(command);

        return this.NoContent();
    }

    [HttpGet("companies/{company}/parent")]
    [ProducesResponseType(typeof(CompanyPresentation), StatusCodes.Status200OK)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    [ProblemResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Parent([FromRoute(Name = "company")] Guid companyId)
    {
        var query = new CompanyParentQuery(companyId);
        var parentCompany = await this.mediator.Send(query);

        return parentCompany is not null ? this.Ok(parentCompany) : this.NotFound();
    }

    [HttpGet("companies/{company}/children")]
    [ProducesResponseType(typeof(CompanyPresentation), StatusCodes.Status200OK)]
    [ProblemResponseType(StatusCodes.Status400BadRequest)]
    [ProblemResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Children([FromRoute(Name = "company")] Guid companyId)
    {
        var query = new CompanyChildrenQuery(companyId);
        var companyChildren = await this.mediator.Send(query);

        return this.Ok(companyChildren);
    }
}