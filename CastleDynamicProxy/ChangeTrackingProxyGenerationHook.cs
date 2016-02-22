using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace CastleDynamicProxy
{
    internal class ChangeTrackingProxyGenerationHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {

        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {

        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return !methodInfo.Name.StartsWith("get_", StringComparison.Ordinal);
        }
    }
}