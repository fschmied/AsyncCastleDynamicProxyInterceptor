using System.Reflection;
using System.Threading.Tasks;
using AsyncInterceptor.CastleDynamicProxySimulation;

namespace AsyncInterceptor.AsyncDynamicProxyExtensions
{
    class AsyncToSyncInterceptorAdapter : ISyncInterceptor
    {
        private IAsyncInterceptor _asyncInterceptor;

        public AsyncToSyncInterceptorAdapter(IAsyncInterceptor asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }

        public void Intercept(ISyncInvocation invocation)
        {
            var asyncInvocation = new SyncToAsyncVoidInvocationAdapter(invocation);
            invocation.Result = _asyncInterceptor.Intercept(asyncInvocation);
        }

        class SyncToAsyncVoidInvocationAdapter : IAsyncVoidInvocation
        {
            private ISyncInvocation _syncInvocation;

            public SyncToAsyncVoidInvocationAdapter(ISyncInvocation syncInvocation)
            {
                _syncInvocation = syncInvocation;
            }

            public object[] Arguments { get => _syncInvocation.Arguments; set => _syncInvocation.Arguments = value; }

            public MethodInfo Method => _syncInvocation.Method;

            public Task Proceed()
            {
                _syncInvocation.Proceed();
                return (Task) _syncInvocation.Result;
            }
        }
    }
}
