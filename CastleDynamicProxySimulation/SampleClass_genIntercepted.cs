using System.Reflection;
using System.Threading.Tasks;

namespace AsyncInterceptor.CastleDynamicProxySimulation
{
    class SampleClass_genIntercepted : SampleClass
    {
        private readonly ISyncInterceptor _interceptor;

        public SampleClass_genIntercepted(ISyncInterceptor interceptor)
        {
            this._interceptor = interceptor;
        }

        public override Task Foo(int value)
        {
            var invocation = new FooInvocation(this, new object[] { value });
            _interceptor.Intercept(invocation);
            return (Task)invocation.Result;
        }

        private Task base_Foo(int value)
        {
            return base.Foo(value);
        }

        private class FooInvocation : ISyncInvocation
        {
            private readonly SampleClass_genIntercepted _instance;
            public object[] Arguments { get; set; }

            public FooInvocation(SampleClass_genIntercepted instance, object[] arguments)
            {
                _instance = instance;
                Arguments = arguments;
                Result = null;
                Method = typeof(SampleClass).GetMethod("Foo");
            }

            public object Result { get; set; }

            public MethodInfo Method { get; }

            public void Proceed()
            {
                Result = _instance.base_Foo((int) Arguments[0]);
            }
        }
    }
}
