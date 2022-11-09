using System.ComponentModel.DataAnnotations;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.AddMember;

public sealed record AddMemberRequest
{
    [Required]
    public Guid MemberId { get; init; }
}