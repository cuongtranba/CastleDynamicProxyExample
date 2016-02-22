using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace CastleDynamicProxy
{
    internal class ChangeTrackingInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            throw new NotImplementedException();
        }
    }
}