using System.Text;

namespace GB.AccessManagement.WebApi.AcceptanceTests.Stubs.HttpClientFactories;

[Serializable]
public sealed class MissingStubResponseException : Exception
{
    public MissingStubResponseException(HttpRequestKey key) : base() { }

    private static string BuildErrorMessage(HttpRequestKey key)
    {
        return new StringBuilder("Missing stub response for request ")
            .Append($"{key.Method}: {key.Scheme}://{key.Authority}{key.PathAndQuery}")
            .ToString();
    }
}