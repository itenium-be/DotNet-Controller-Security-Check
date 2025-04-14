using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Itenium.Tests;

public class MinimalWebApiTests
{
    [Fact]
    public void CheckMinimalWebApiEndpoints()
    {
        var factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { /* Mock services if needed */ });
            });

        // var client = factory.CreateClient();
        var endpointDataSource = factory.Services.GetRequiredService<EndpointDataSource>();

        foreach (var endpoint in endpointDataSource.Endpoints)
        {
            var metadata = endpoint.Metadata;
            bool isAuthorized = metadata.GetMetadata<IAuthorizeData>() != null;
            bool isAnonymousAllowed = metadata.GetMetadata<IAllowAnonymous>() != null;

            Assert.True(
                isAuthorized || isAnonymousAllowed,
                $"🔴 Endpoint '{endpoint.DisplayName}' is insecure! " +
                "Add .RequireAuthorization() or .AllowAnonymous()."
            );
        }
    }
}