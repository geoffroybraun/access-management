using System.Net;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Stubs.HttpClientFactories;

public sealed class HttpClientFactoryStub : HttpMessageHandler, IHttpClientFactory, IStubService
{
    private readonly Dictionary<HttpRequestKey, HttpResponseMessage> responses = new();
    
    public HttpClient CreateClient(string name)
    {
        return new(this);
    }

    public void Add(string scheme, string authority, string pathAndQuery, HttpMethod method, HttpStatusCode statusCode, HttpContent content)
    {
        HttpRequestKey key = new(scheme, authority, pathAndQuery, method);

        if (responses.ContainsKey(key))
        {
            return;
        }
        
        var response = new HttpResponseMessage(statusCode) { Content = content };
        responses.Add(key, response);
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpRequestKey key = request;

        if (responses.TryGetValue(key, out HttpResponseMessage? response))
        {
            return Task.FromResult(response);
        }

        throw new MissingStubResponseException(key);
    }
}