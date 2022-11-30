using GB.AccessManagement.Accesses.Domain.Evaluators;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.Extensions.Options;
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
        using var api = this.factory.CreateApi(this.options);
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