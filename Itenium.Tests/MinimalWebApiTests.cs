using Itenium.AuthCheck;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Itenium.Tests;

public class MinimalWebApiTests
{
    [Fact]
    public void CheckMinimalWebApiEndpoints()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services => { /* Mock services as needed */ });
        });

        string[] unsecured = FindUnsecuredMinimalWebApiEndpoints.GetEndpoints(factory.Services)
            .Where(e => !e.Contains("/openapi/") && !e.Contains("/scalar/"))
            .ToArray();

        Assert.Single(unsecured);
        Assert.Equal("HTTP: GET /unsecured", unsecured[0]);
    }

    [Fact]
    public void MinimalWebApiEndpoints_DoesNotReturnAnonymousAndWithRequiredAuthorization()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services => { /* Mock services as needed */ });
        });

        string[] unsecured = FindUnsecuredMinimalWebApiEndpoints.GetEndpoints(factory.Services)
            .Where(e => !e.Contains("/openapi/") && !e.Contains("/scalar/"))
            .ToArray();
        Assert.Single(unsecured);
        Assert.Equal("HTTP: GET /unsecured", unsecured[0]);
    }
}