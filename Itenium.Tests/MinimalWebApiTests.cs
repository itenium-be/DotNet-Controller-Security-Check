using Itenium.AuthCheck;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Itenium.Tests;

public class MinimalWebApiTests
{
    /// <summary>
    /// This test fails because we have an unsecured endpoint
    /// </summary>
    [Fact]
    public void CheckMinimalWebApiEndpoints()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services => { /* Mock services as needed */ });
        });

        string[] unsecured = FindUnsecuredMinimalWebApiEndpoints.GetEndpoints(factory.Services).ToArray();
        if (unsecured.Any())
        {
            Assert.Fail($"Add .RequireAuthorization() or .AllowAnonymous() to the following endpoints: \n{string.Join(", ", unsecured)}");
        }
    }

    [Fact]
    public void MinimalWebApiEndpoints_DoesNotReturnAnonymousAndWithRequiredAuthorization()
    {
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services => { /* Mock services as needed */ });
        });

        string[] unsecured = FindUnsecuredMinimalWebApiEndpoints.GetEndpoints(factory.Services).ToArray();
        Assert.Single(unsecured);
        Assert.Equal("HTTP: GET /unsecured", unsecured[0]);
    }
}