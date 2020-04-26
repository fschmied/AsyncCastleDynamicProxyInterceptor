using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AsyncInterceptor.AsyncDynamicProxyExtensions
{
    interface IAsyncInterceptor
    {
        void OnBeforeProceed(MethodInfo method, object[] arguments, out object state);
        void OnExceptionInProceed(MethodInfo method, object[] arguments, object state, Exception ex, out bool rethrow);
        Task OnFinallyAfterProceed(MethodInfo method, object[] arguments, object state);
        Task OnAfterSuccessfulProceed(MethodInfo method, object[] arguments, object state);
    }
}
