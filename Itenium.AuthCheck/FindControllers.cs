using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Itenium.AuthCheck;

public static class FindControllers
{
    /// <summary>
    /// Return all types that have a GetType().Name that ends with "Controller"
    /// </summary>
    public static Type[] FindByName(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Controller"))
            .ToArray();
    }

    /// <summary>
    /// Returns all types that (indirectly) inherit from <see cref="ControllerBase"/>
    /// </summary>
    public static Type[] FindByBaseClass(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
            .ToArray();
    }

    /// <summary>
    /// Returns all types that have the <see cref="ApiControllerAttribute"/>
    /// </summary>
    public static Type[] FindByAttribute(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(ApiControllerAttribute), true).Any())
            .ToArray();
    }
}