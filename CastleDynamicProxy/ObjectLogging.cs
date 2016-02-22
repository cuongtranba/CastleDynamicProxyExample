using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using Castle.DynamicProxy;

namespace CastleDynamicProxy
{
    public static class LoggingProxy
    {

        private static ProxyGenerationOptions _Options;

        public static string ChangeInfor { get; private set; }

        static LoggingProxy ()
        {
            _Options = new ProxyGenerationOptions
            {
                Hook = new ChangeTrackingProxyGenerationHook(),
                //Selector = new ChangeTrackingInterceptorSelector()
            };
        }

        public static T Wrap<T>(T target) where T : class
        {
            var objectInterceptor=new ObjectInterceptor();
            ProxyGenerator generator = new ProxyGenerator();

            objectInterceptor.BeforeExecute += ObjectProxy_BeforeExecute;
            //objectInterceptor.AfterExecute += ObjectProxy_AfterExecute;
            objectInterceptor.ErrorExecute += ObjectProxy_ErrorExecute;
            return (T) generator.CreateClassProxy(typeof (T), _Options, objectInterceptor);
        }
        //todo implement logic here
        private static void ObjectProxy_ErrorExecute(object sender, Exception e)
        {
            Console.WriteLine(e.Message);
        }
        //todo implement logic here
        private static void ObjectProxy_BeforeExecute(object sender, IInvocation e)
        {
            ChangeInfor= $"method call: {e.Method.Name} - parameter: {e.Arguments[0]}";
        }
    }

    class ObjectInterceptor : IInterceptor
    {
        public event EventHandler<IInvocation> AfterExecute;

        public event EventHandler<IInvocation> BeforeExecute;

        public event EventHandler<Exception> ErrorExecute;

        private void OnAfterExecute(IInvocation methodCall)
        {
            AfterExecute?.Invoke(this, methodCall);
        }

        private void OnBeforeExecute(IInvocation methodCall)
        {
            BeforeExecute?.Invoke(this, methodCall);
        }

        private void OnErrorExecute(Exception exception)
        {
            ErrorExecute?.Invoke(this, exception);
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                OnBeforeExecute(invocation);
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                OnErrorExecute(exception);
            }
            finally
            {
                OnAfterExecute(invocation);
            }
        }
    }

}
