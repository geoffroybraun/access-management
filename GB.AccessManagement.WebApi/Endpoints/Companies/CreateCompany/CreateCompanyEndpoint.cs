using GB.AccessManagement.Companies.Contracts.Commands;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CreateCompany;

public sealed class CreateCompanyEndpoint : IEndpoint<CreateCompanyCommand>
{
    private readonly IMediator mediator;

    public CreateCompanyEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(CreateCompanyCommand request)
    {
        var companyId = await this.mediator.Send(request);
        string companyUri = $"/v1/users/{request.OwnerId}/companies/{companyId}";

        return Results.Created(companyUri, new
        {
            name = request.Name,
            ownerId = request.OwnerId
        });
    }
}