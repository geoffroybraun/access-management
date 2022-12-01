using System.Net;
using System.Net.Http.Json;
using GB.AccessManagement.Accesses.Infrastructure;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Stubs.HttpClientFactories;

public sealed class HttpClientFactoryStubHostedService : IHostedService
{
    private readonly HttpClientFactoryStub stub;
    private readonly OpenFgaOptions options;

    public HttpClientFactoryStubHostedService(HttpClientFactoryStub stub, IOptionsMonitor<OpenFgaOptions> options)
    {
        this.stub = stub;
        this.options = options.CurrentValue;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.AddStoresResponse();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void AddStoresResponse()
    {
        var response = new ListStoresResponse(new List<Store>
        {
            new Store("ID", this.options.StoreName)
        });
        var content = JsonContent.Create(response);
        
        stub.Add(this.options.Scheme, this.options.Host, "/stores", HttpMethod.Get, HttpStatusCode.OK, content);
    }
}