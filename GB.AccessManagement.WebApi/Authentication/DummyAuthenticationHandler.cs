using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace GB.AccessManagement.WebApi.Authentication;

public sealed class DummyAuthenticationHandler : AuthenticationHandler<DummyAuthenticationOptions>
{
    private const string UserId = "A2F7CC44-7646-4D0D-ACFF-156340DB62A5";
    public const string AuthenticationScheme = "Bearer";
    
    public DummyAuthenticationHandler(
        IOptionsMonitor<DummyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!HasAuthorizationHeader(out var authorizationHeader))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        string[] headerValues = authorizationHeader
            .ToArray()
            .First()
            .Split(' ');

        if (!HasAccurateAuthenticationScheme(headerValues))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (!IsTokenValid(headerValues))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
        }

        var claims = new Claim[] { new(ClaimTypes.NameIdentifier, UserId) };
        var identity = new ClaimsIdentity(claims, AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
    
    private bool HasAuthorizationHeader(out StringValues authorizationHeader)
    {
        return this.Context
            .Request
            .Headers
            .TryGetValue(HeaderNames.Authorization, out authorizationHeader);
    }

    private static bool HasAccurateAuthenticationScheme(string[] headerValues)
    {
        return headerValues
            .First()
            .Equals(AuthenticationScheme, StringComparison.OrdinalIgnoreCase);
    }

    private static bool IsTokenValid(string[] headerValues)
    {
        return headerValues
            .Last()
            .Equals("token", StringComparison.OrdinalIgnoreCase);
    }
}