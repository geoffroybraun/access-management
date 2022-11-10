using GB.AccessManagement.Accesses.Domain.Evaluators;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Configuration;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Evaluators;

public sealed class OpenFgaUserAccessEvaluator : IUserAccessEvaluator, ISingletonService
{
    private readonly OpenFgaOptions options;
    private readonly IHttpClientFactory factory;

    public OpenFgaUserAccessEvaluator(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.options = options.Value;
        this.factory = factory;
    }
    
    public async Task<bool> CanAccess(UserAccess access)
    {
        var configuration = new Configuration
        {
            ApiHost = this.options.Host,
            ApiScheme = this.options.Scheme,
            StoreId = this.options.StoreId
        };
        using var client = this.factory.CreateClient();
        using OpenFgaApi api = new(configuration, client);
        var response = await api.Check(new CheckRequest
        {
            TupleKey = new()
            {
                Object = $"{access.ObjectType}:{access.ObjectId}",
                Relation = access.Relation,
                User = access.UserId
            }
        });

        return response.Allowed == true;
    }
}