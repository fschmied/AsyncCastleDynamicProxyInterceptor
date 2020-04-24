using System;
using System.Linq;
using System.Threading.Tasks;
using AsyncInterceptor.AsyncDynamicProxyExtensions;

namespace AsyncInterceptor
{
    class SampleAsyncTracingInterceptor : IAsyncInterceptor
    {
        public async Task Intercept(IAsyncVoidInvocation invocation)
        {
            Console.WriteLine("Async Intercepting " + invocation.Method + "(" + string.Join(", ", invocation.Arguments.Select(x => x.ToString())) + ")");
            try
            {
                await invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine("On noes, Async Intercepted an exception: " + ex.Message);
                throw;
            }
            Console.WriteLine("Async Intercepted " + invocation.Method);
        }
    }
}
