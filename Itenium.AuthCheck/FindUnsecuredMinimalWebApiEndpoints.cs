using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Itenium.AuthCheck;

public static class FindUnsecuredMinimalWebApiEndpoints
{
    /// <summary>
    /// Get the DisplayName "HTTP: GET /uri-route" for all minimal WebApi
    /// endpoints that do not have RequireAuthorization nor AllowAnonymous
    /// </summary>
    public static IEnumerable<string> GetEndpoints(IServiceProvider serviceProvider)
    {
        var endpointDataSource = serviceProvider.GetRequiredService<EndpointDataSource>();

        foreach (var endpoint in endpointDataSource.Endpoints)
        {
            var metadata = endpoint.Metadata;
            bool isAuthorized = metadata.GetMetadata<IAuthorizeData>() != null;
            bool isAnonymousAllowed = metadata.GetMetadata<IAllowAnonymous>() != null;

            if (!isAuthorized && !isAnonymousAllowed)
            {
                yield return endpoint.DisplayName ?? "??";
            }
        }
    }
}