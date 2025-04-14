using System.Reflection;
using Itenium.AuthCheck;
using Itenium.WebApi.Controllers;

namespace Itenium.Tests;

public class ControllerAuthorizationTests
{
    /// <summary>
    /// This test fails because well, we added some controller action methods without security!
    /// </summary>
    [Fact]
    public void CheckThatAllControllerActionMethods_HaveAuthorizeAttribute()
    {
        Assembly assembly = typeof(ZeroSecurityController).Assembly;
        Type[] controllerTypes = FindControllers.FindByName(assembly);
        string[] missingAuthActions = FindUnsecuredActionMethods.GetUnsecuredMethodNames(controllerTypes).ToArray();

        if (missingAuthActions.Length > 0)
        {
            var sortedMethods = missingAuthActions.OrderBy(x => x);
            Assert.Fail($"The following actions are missing [Authorize] attribute:\n{string.Join("\n", sortedMethods)}");
        }

        Assert.Empty(missingAuthActions);
    }


    [Fact]
    public void ControllerAndActionMethodWithoutAnyAttribute_IsReturned()
    {
        Type controller = typeof(ZeroSecurityController);
        string[] missingAuthActions = FindUnsecuredActionMethods.GetUnsecuredMethodNames(controller).ToArray();

        Assert.Single(missingAuthActions);
        Assert.Equal("ZeroSecurityController.Get", missingAuthActions[0]);
    }

    [Fact]
    public void AnonymousController_ReturnsNoMethods()
    {
        Type controller = typeof(AnonymousController);
        string[] missingAuthActions = FindUnsecuredActionMethods.GetUnsecuredMethodNames(controller).ToArray();
        Assert.Empty(missingAuthActions);
    }

    [Fact]
    public void NotImplementedController_MethodsThrowingNotImplementedException_AreNotReturned()
    {
        Type controller = typeof(NotImplementedController);
        string[] missingAuthActions = FindUnsecuredActionMethods.GetUnsecuredMethodNames(controller).ToArray();
        Assert.Empty(missingAuthActions);
    }

    [Fact]
    public void MethodsWithAllowAnonymousOrAuthorizeAttributes_AreNotReturned()
    {
        Type controller = typeof(ActionsController);
        string[] missingAuthActions = FindUnsecuredActionMethods.GetUnsecuredMethodNames(controller).ToArray();
        Assert.Empty(missingAuthActions);
    }
}
