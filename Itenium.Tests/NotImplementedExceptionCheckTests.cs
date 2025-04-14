using System.Reflection;
using Itenium.AuthCheck;
using Itenium.WebApi.Controllers;

namespace Itenium.Tests;

public class NotImplementedExceptionCheckTests
{
    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;

    [Fact]
    public void MethodThrowsNewNotImplementedException_ReturnsFalse()
    {
        var method = typeof(ActionsController).GetMethod(nameof(NotImplementedController.Get), Flags)!;
        bool result = NotImplementedExceptionCheck.Check(method);
        Assert.False(result);
    }

    [Fact]
    public void MethodThrowsNewNotImplementedException_IsDetected()
    {
        var method = typeof(NotImplementedController).GetMethod(nameof(NotImplementedController.Get), Flags)!;
        bool result = NotImplementedExceptionCheck.Check(method);
        Assert.True(result);
    }

    [Fact]
    public void MethodThrowsNewNotImplementedExceptionWithMessage_IsDetected()
    {
        var method = typeof(NotImplementedController).GetMethod(nameof(NotImplementedController.Post), Flags)!;
        bool result = NotImplementedExceptionCheck.Check(method);
        Assert.True(result);
    }

    [Fact]
    public void MethodThrowsNewNotImplementedExceptionWithAssignment_IsNotDetected()
    {
        // More hacky code needed for cases like this...
        var method = typeof(NotImplementedExceptionCheckTests).GetMethod(nameof(Undetected), BindingFlags.NonPublic | BindingFlags.Static)!;
        bool result = NotImplementedExceptionCheck.Check(method);
        Assert.False(result);
    }

    private static void Undetected()
    {
        var ex = new NotImplementedException();
        throw ex;
    }
}