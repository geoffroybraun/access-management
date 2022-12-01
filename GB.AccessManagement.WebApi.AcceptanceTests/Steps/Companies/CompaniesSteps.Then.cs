using System.Net;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Steps.Companies;

public sealed partial class CompaniesSteps
{
    [Then(@"an unauthorized error status code is returned")]
    public void ThenAnUnauthorizedErrorStatusCodeIsReturned()
    {
        this.context
            .Get<HttpResponseMessage>("Response")
            .Should()
            .HaveStatusCode(HttpStatusCode.Unauthorized);
    }
}