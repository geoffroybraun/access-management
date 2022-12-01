using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Steps;

[Binding]
public sealed class HookSteps : IDisposable
{
    private readonly ScenarioContext context;
    private TestServer? testServer;
    private bool? hasDisposed;

    public HookSteps(ScenarioContext context)
    {
        this.context = context;
    }

    [BeforeScenario]
    public void Initialize()
    {
        this.testServer = Create();
        this.context.Set(testServer.CreateClient(), "TestServer.HttpClient");
        this.context.Set(Guid.NewGuid(), "TestServer.Id");
    }

    [AfterScenario]
    public void Dispose()
    {
        this.Dispose(true);
    }

    private void Dispose(bool isDisposing)
    {
        if (!isDisposing || this.hasDisposed == true)
        {
            return;
        }
        
        this.testServer!.Dispose();
        (this.context as IDisposable).Dispose();
        this.hasDisposed = true;
    }

    private static TestServer Create()
    {
        var builder = new WebHostBuilder()
            .UseStartup<Startup>()
            .ConfigureAppConfiguration(configuration => configuration.AddJsonFile("appsettings.json"));
        
        return new(builder);
    }
}