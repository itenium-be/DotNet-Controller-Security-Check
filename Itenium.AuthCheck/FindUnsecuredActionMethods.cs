using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Itenium.AuthCheck;

public static class FindUnsecuredActionMethods
{
    public static IEnumerable<string> GetUnsecuredMethodNames(params Type[] controllers)
    {
        foreach (Type controllerType in controllers)
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
                    yield return $"{controllerType.Name}.{method.Name}";
                }
            }
        }
    }
}