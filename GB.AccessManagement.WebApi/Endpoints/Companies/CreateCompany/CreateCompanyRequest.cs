using System.ComponentModel.DataAnnotations;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CreateCompany;

public sealed record CreateCompanyRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
}