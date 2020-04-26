using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AsyncInterceptor.AsyncDynamicProxyExtensions;

namespace AsyncInterceptor
{
    class SampleAsyncTracingInterceptor : IAsyncInterceptor
    {
        public void OnBeforeProceed(MethodInfo method, object[] arguments, out object state)
        {
            Console.WriteLine("Async Intercepting " + method + "(" + string.Join(", ", arguments.Select(x => x.ToString())) + ")");
            state = null;
        }

        public Task OnAfterSuccessfulProceed(MethodInfo method, object[] arguments, object state)
        {
            Console.WriteLine("Async Intercepted " + method);
            return Task.CompletedTask;
        }

        public void OnExceptionInProceed(MethodInfo method, object[] arguments, object state, Exception ex, out bool rethrow)
        {
            Console.WriteLine("On noes, Async Intercepted an exception: " + ex.Message);
            rethrow = true;
        }

        public Task OnFinallyAfterProceed(MethodInfo method, object[] arguments, object state)
        {
            Console.WriteLine("Async Finally after Proceed " + method);
            return Task.CompletedTask;
        }
    }
}
