using GB.AccessManagement.Accesses.Infrastructure.Extensions;
using GB.AccessManagement.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OpenFga.Sdk.Model;

namespace GB.AccessManagement.Accesses.Infrastructure.Middlewares;

public sealed class OpenFgaMiddleware : IMiddleware, ISelfTransientService
{
    private readonly IHttpClientFactory factory;
    private readonly OpenFgaOptions options;

    public OpenFgaMiddleware(IOptions<OpenFgaOptions> options, IHttpClientFactory factory)
    {
        this.factory = factory;
        this.options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        this.options.StoreId = await this.GetStoreId();

        await next(context);
    }

    private async Task<string> GetStoreId()
    {
        using var api = this.factory.CreateApi(this.options);
        var response = await api.ListStores();

        if (response.Stores is not null && response.Stores.Any(HasStoreNamedAfterOptions))
        {
            return response
                .Stores
                .Single(HasStoreNamedAfterOptions)
                .Id!;
        }

        var createStoreResponse = await api.CreateStore(new CreateStoreRequest() { Name = this.options.StoreName });

        return createStoreResponse.Id!;
    }

    private bool HasStoreNamedAfterOptions(Store store)
    {
        return !string.IsNullOrEmpty(store.Name)
            && store.Name.Equals(this.options.StoreName, StringComparison.OrdinalIgnoreCase);
    }
}