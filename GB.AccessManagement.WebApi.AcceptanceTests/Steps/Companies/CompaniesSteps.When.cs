using System.Net.Http.Json;
using TechTalk.SpecFlow;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Steps.Companies;

[Binding]
public sealed partial class CompaniesSteps
{
    private readonly ScenarioContext context;

    public CompaniesSteps(ScenarioContext context)
    {
        this.context = context;
    }

    [When(@"a user creates a company named ""(.*)""")]
    public async Task WhenAUserCreatesACompanyNamed(string company)
    {
        Guid userId = this.context.Get<Guid>("TestServer.Id");
        
        using var client = context.Get<HttpClient>("TestServer.HttpClient");
        var response = await client.PostAsync($"v1/users/{userId}/companies", JsonContent.Create(new
        {
            name = company
        }));
        
        context.Set(response, "Response");
    }
}