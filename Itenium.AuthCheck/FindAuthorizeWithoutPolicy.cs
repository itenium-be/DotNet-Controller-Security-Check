using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Itenium.AuthCheck;

public static class FindAuthorizeWithoutPolicy
{
    public static IEnumerable<string> GetMethodsWithoutPolicy(params Type[] controllers)
    {
        foreach (Type controllerType in controllers)
        {
            var controllerAuthorize = controllerType.GetCustomAttribute<AuthorizeAttribute>(true);
            bool controllerHasPolicy = !string.IsNullOrEmpty(controllerAuthorize?.Policy);

            // If controller has [Authorize] without policy, report it
            if (controllerAuthorize != null && !controllerHasPolicy)
            {
                yield return controllerType.Name;
            }

            // Skip methods if controller already has a policy
            if (controllerHasPolicy)
                continue;

            var methods = controllerType
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                .Where(m => !m.GetCustomAttributes(typeof(NonActionAttribute), true).Any());

            foreach (MethodInfo method in methods)
            {
                var methodAuthorize = method.GetCustomAttribute<AuthorizeAttribute>(true);
                if (methodAuthorize != null && string.IsNullOrEmpty(methodAuthorize.Policy))
                {
                    yield return $"{controllerType.Name}.{method.Name}";
                }
            }
        }
    }
}
