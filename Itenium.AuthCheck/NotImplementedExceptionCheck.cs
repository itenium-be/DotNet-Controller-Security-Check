using System.Reflection;

namespace Itenium.AuthCheck;

public static class NotImplementedExceptionCheck
{
    /// <summary>
    /// Returns True when the method merely throws a <see cref="NotImplementedException"/>
    /// </summary>
    public static bool Check(MethodInfo method)
    {
        byte[]? methodBody = method.GetMethodBody()?.GetILAsByteArray();
        if (methodBody == null)
            return false;

        for (int i = 0; i < methodBody.Length - 5; i++)
        {
            // Look for NEWOBJ (0x73) followed later by THROW (0x7A)
            if (methodBody[i] == 0x73 && methodBody[i + 5] == 0x7A)
            {
                var ctorToken = BitConverter.ToInt32(methodBody, i + 1);
                var ctor = method.Module.ResolveMethod(ctorToken) as ConstructorInfo;
                if (ctor?.DeclaringType == typeof(NotImplementedException))
                    return true;
            }
        }
        return false;
    }
}
