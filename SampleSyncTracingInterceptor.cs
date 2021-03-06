using System;
using System.Linq;
using Castle.DynamicProxy;

namespace AsyncInterceptor
{
    class SampleSyncTracingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("Sync Intercepting " + invocation.Method + "(" + string.Join(", ", invocation.Arguments.Select(x => x.ToString())) + ")");
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("On noes, Sync Intercepted an exception: " + ex.Message);
                throw;
            }
            Console.WriteLine("Sync Intercepted " + invocation.Method);
            Console.WriteLine("Result: " + invocation.ReturnValue);
        }
    }
}
