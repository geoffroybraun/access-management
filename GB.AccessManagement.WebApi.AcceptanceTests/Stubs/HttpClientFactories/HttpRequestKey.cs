namespace GB.AccessManagement.WebApi.AcceptanceTests.Stubs.HttpClientFactories;

public sealed record HttpRequestKey(string Scheme, string Authority, string PathAndQuery, HttpMethod Method)
{
    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = GetHashCode(Scheme);
            hashCode = ComputeHashCode(hashCode, Authority);
            hashCode = ComputeHashCode(hashCode, PathAndQuery);

            return ComputeHashCode(hashCode, Method.ToString());
        }
    }

    public static implicit operator HttpRequestKey(HttpRequestMessage request)
    {
        return new(
            request.RequestUri!.Scheme,
            request.RequestUri!.Authority,
            request.RequestUri!.PathAndQuery,
            request.Method);
    }

    private static int ComputeHashCode(int hashCode, string value)
    {
        return (hashCode * 397) ^ GetHashCode(value);
    }

    private static int GetHashCode(string value)
    {
        return string.IsNullOrEmpty(value)
            ? 0
            : StringComparer.Ordinal.GetHashCode(value);
    }
}