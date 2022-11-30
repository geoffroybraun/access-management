using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GB.AccessManagement.WebApi.Controllers.Attributes;

public sealed class ProblemResponseTypeAttribute : SwaggerResponseAttribute
{
    private const string ContentType = "application/problem+json";
    private static readonly Type Problem = typeof(ProblemDetails);
    
    public ProblemResponseTypeAttribute(int statusCode) : base(statusCode, type: Problem, contentTypes: ContentType) { }
}