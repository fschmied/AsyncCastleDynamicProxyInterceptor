using System;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace AsyncInterceptor.AsyncDynamicProxyExtensions
{
    class AsyncToSyncInterceptorAdapter : IInterceptor
    {
        private IAsyncInterceptor _asyncInterceptor;

        public AsyncToSyncInterceptorAdapter(IAsyncInterceptor asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            var asyncInvocation = new SyncToAsyncVoidInvocationAdapter(invocation);
            var asyncInterceptorTask = _asyncInterceptor.Intercept(asyncInvocation);
            invocation.ReturnValue = asyncInterceptorTask;
        }

        class SyncToAsyncVoidInvocationAdapter : IAsyncVoidInvocation
        {
            private IInvocation _syncInvocation;

            public SyncToAsyncVoidInvocationAdapter(IInvocation syncInvocation)
            {
                _syncInvocation = syncInvocation;
            }

            public object[] Arguments { get => _syncInvocation.Arguments; }

            public MethodInfo Method => _syncInvocation.Method;

            public Task Proceed()
            {
                _syncInvocation.Proceed();
                return (Task) _syncInvocation.ReturnValue;
            }
        }
    }
}
