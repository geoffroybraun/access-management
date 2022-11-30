using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Controllers.Attributes;

public sealed class ConsumesJsonAttribute : ConsumesAttribute
{
    private const string ContentType = "application/json";

    public ConsumesJsonAttribute() : base(ContentType) { }
}