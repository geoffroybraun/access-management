using GB.AccessManagement.WebApi.AcceptanceTests.Stubs;
using GB.AccessManagement.WebApi.AcceptanceTests.Stubs.HttpClientFactories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
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
            .ConfigureAppConfiguration(AddAppSettings)
            .ConfigureServices(AddStubHostedService)
            .ConfigureTestServices(AddStubServices);
        
        return new(builder);
    }

    private static void AddAppSettings(IConfigurationBuilder builder)
    {
        builder.AddJsonFile("appsettings.json");
    }

    private static void AddStubHostedService(IServiceCollection services)
    {
        services.AddHostedService<HttpClientFactoryStubHostedService>();
    }

    private static void AddStubServices(IServiceCollection services)
    {
        services.Scan(selector =>
        {
            _ = selector
                .FromAssemblies(typeof(HookSteps).Assembly)
                .AddClasses(classes => classes.AssignableTo<IStubService>())
                .UsingRegistrationStrategy(RegistrationStrategy.Replace())
                .AsSelfWithInterfaces()
                .WithSingletonLifetime();
        });
    }
}