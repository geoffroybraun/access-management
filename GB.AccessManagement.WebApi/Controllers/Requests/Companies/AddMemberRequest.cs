using System.ComponentModel.DataAnnotations;

namespace GB.AccessManagement.WebApi.Controllers.Requests.Companies;

public sealed record AddMemberRequest
{
    [Required]
    public Guid MemberId { get; init; }
}