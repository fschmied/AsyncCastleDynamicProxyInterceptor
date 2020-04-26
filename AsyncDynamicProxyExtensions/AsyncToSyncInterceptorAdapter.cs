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
            var asyncInterceptorTask = AsyncIntercept(asyncInvocation);
            invocation.ReturnValue = asyncInterceptorTask;
        }

        private async Task AsyncIntercept(IAsyncVoidInvocation asyncInvocation)
        {
            // Note: There must not be an await before asyncVoidInvocation.Proceed because DynamicProxy
            // will reset what Proceed does once the method returns synchronously (which an await would do).
            _asyncInterceptor.OnBeforeProceed(asyncInvocation.Method, asyncInvocation.Arguments, out var state);

            try
            {
                await asyncInvocation.Proceed();
            }
            catch (Exception ex)
            {
                _asyncInterceptor.OnExceptionInProceed(asyncInvocation.Method, asyncInvocation.Arguments, state, ex, out var rethrow);
                if (rethrow)
                    throw;
            }
            finally
            {
                await _asyncInterceptor.OnFinallyAfterProceed(asyncInvocation.Method, asyncInvocation.Arguments, state);
            }

            await _asyncInterceptor.OnAfterSuccessfulProceed(asyncInvocation.Method, asyncInvocation.Arguments, state);
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
