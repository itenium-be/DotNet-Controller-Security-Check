using System.Reflection;
using Itenium.AuthCheck;
using Itenium.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        Type[] controllerTypes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(ApiControllerAttribute), true).Any())
            .ToArray();

        var missingAuthActions = new List<string>();
        foreach (Type controllerType in controllerTypes)
        {
            // If the entire Controller Authorize/AllowsAnonymous, just move on
            bool controllerHasAuthorize = controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any();
            if (controllerHasAuthorize)
                continue;

            bool controllerHasAllowAnonymous = controllerType.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
            if (controllerHasAllowAnonymous)
                continue;

            // NonAction methods do not represent an endpoint, and need no security check
            var methods = controllerType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => !m.GetCustomAttributes(typeof(NonActionAttribute), true).Any());

            foreach (MethodInfo method in methods)
            {
                bool hasAuthorize = method.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any();
                bool hasAllowAnonymous = method.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any();
                if (!hasAllowAnonymous && !hasAuthorize && !NotImplementedExceptionCheck.Check(method))
                {
                    missingAuthActions.Add($"{controllerType.Name}.{method.Name}");
                }
            }
        }

        if (missingAuthActions.Any())
        {
            var sortedMethods = missingAuthActions.OrderBy(x => x);
            Assert.Fail($"The following actions are missing [Authorize] attribute:\n{string.Join("\n", sortedMethods)}");
        }

        Assert.Empty(missingAuthActions);
    }

    [Fact]
    public void CheckThatAllControllerActionMethods_HaveAuthorizeAttribute_FullCode()
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
