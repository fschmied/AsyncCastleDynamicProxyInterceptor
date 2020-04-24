using System.Threading.Tasks;

namespace AsyncInterceptor.AsyncDynamicProxyExtensions
{
    interface IAsyncInterceptor
    {
        Task Intercept(IAsyncVoidInvocation invocation);
    }
}
