using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Controllers.Attributes;

public sealed class ProducesJsonAttribute : ProducesAttribute
{
    private const string ContentType = "application/json";

    public ProducesJsonAttribute(): base(ContentType) { }
}