using System.ComponentModel.DataAnnotations;

namespace GB.AccessManagement.WebApi.Controllers.Requests.Companies;

public sealed record CreateCompanyRequest
{
    [Required]
    public string Name { get; init; } = string.Empty;
}