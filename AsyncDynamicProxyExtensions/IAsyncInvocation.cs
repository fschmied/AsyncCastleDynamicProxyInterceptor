// Note: This currently supports only Task, not Task<T>, as getting the types right is difficult and would require Reflection.
using System.Reflection;
using System.Threading.Tasks;

namespace AsyncInterceptor.AsyncDynamicProxyExtensions
{
    interface IAsyncVoidInvocation
    {
        object[] Arguments { get; }

        MethodInfo Method { get; }
        Task Proceed();
    }
}