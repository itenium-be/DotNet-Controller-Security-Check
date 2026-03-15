using System.Reflection;
using Itenium.AuthCheck;
using Itenium.WebApi.Controllers;

namespace Itenium.Tests;

public class AuthorizeWithoutPolicyTests
{
    [Fact]
    public void MethodWithAuthorizeWithoutPolicy_IsReturned()
    {
        var result = FindAuthorizeWithoutPolicy.GetMethodsWithoutPolicy(typeof(AuthorizeNoPolicyController)).ToArray();

        Assert.Single(result);
        Assert.Equal("AuthorizeNoPolicyController.Get", result[0]);
    }

    [Fact]
    public void MethodWithAuthorizeWithPolicy_IsNotReturned()
    {
        var result = FindAuthorizeWithoutPolicy.GetMethodsWithoutPolicy(typeof(ActionsController)).ToArray();

        Assert.Empty(result);
    }

    [Fact]
    public void ControllerWithAuthorizeWithoutPolicy_IsReturned()
    {
        var result = FindAuthorizeWithoutPolicy.GetMethodsWithoutPolicy(typeof(ControllerLevelNoPolicyController)).ToArray();

        Assert.Single(result);
        Assert.Equal("ControllerLevelNoPolicyController", result[0]);
    }

    [Fact]
    public void ControllerWithAuthorizeWithPolicy_IsNotReturned()
    {
        var result = FindAuthorizeWithoutPolicy.GetMethodsWithoutPolicy(typeof(ControllerLevelWithPolicyController)).ToArray();

        Assert.Empty(result);
    }

    [Fact]
    public void CheckAllControllers_FindsAuthorizeWithoutPolicy()
    {
        Assembly assembly = typeof(ZeroSecurityController).Assembly;
        Type[] controllerTypes = FindControllers.FindByName(assembly);

        var result = FindAuthorizeWithoutPolicy.GetMethodsWithoutPolicy(controllerTypes).ToArray();

        Assert.Contains("AuthorizeNoPolicyController.Get", result);
        Assert.Contains("ControllerLevelNoPolicyController", result);
        Assert.Equal(2, result.Length);
    }
}
