using Itenium.AuthCheck;
using Itenium.WebApi.Controllers;

namespace Itenium.Tests;

public class FindControllersTests
{
    [Fact]
    public void FindByName()
    {
        Type[] controllers = FindControllers.FindByName(typeof(ActionsController).Assembly);
        string[] controllerNames = controllers.Select(x => x.Name).ToArray();
        string[] expected = ["ActionsController", "AnonymousController", "AuthorizeNoPolicyController", "BareController", "ControllerLevelNoPolicyController", "ControllerLevelWithPolicyController", "NotImplementedController", "ZeroSecurityController"];
        Assert.Equal(expected, controllerNames);
    }

    [Fact]
    public void FindByBaseClass()
    {
        Type[] controllers = FindControllers.FindByBaseClass(typeof(ActionsController).Assembly);
        string[] controllerNames = controllers.Select(x => x.Name).ToArray();
        string[] expected = ["ActionsController", "AnonymousController", "AuthorizeNoPolicyController", "ControllerLevelNoPolicyController", "ControllerLevelWithPolicyController", "NotImplementedController", "ZeroSecurityController"];
        Assert.Equal(expected, controllerNames);
    }

    [Fact]
    public void FindByAttribute()
    {
        Type[] controllers = FindControllers.FindByAttribute(typeof(ActionsController).Assembly);
        string[] controllerNames = controllers.Select(x => x.Name).ToArray();
        string[] expected = ["ActionsController", "AnonymousController", "AuthorizeNoPolicyController", "BareController", "ControllerLevelNoPolicyController", "ControllerLevelWithPolicyController", "NotImplementedController", "ZeroSecurityController"];
        Assert.Equal(expected, controllerNames);
    }
}