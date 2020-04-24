using System;
using System.Threading.Tasks;
using AsyncInterceptor.AsyncDynamicProxyExtensions;
using AsyncInterceptor.CastleDynamicProxySimulation;

namespace AsyncInterceptor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await RunFoo (new SampleClass());
            await RunFoo (new SampleClass_genIntercepted(new SampleSyncTracingInterceptor()));
            await RunFoo (new SampleClass_genIntercepted(new AsyncToSyncInterceptorAdapter(new SampleAsyncTracingInterceptor())));
        }

        private static async Task RunFoo(SampleClass instance)
        {
            try
            {
                await instance.Foo(42);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Caught exception!");
                Console.WriteLine(ex);
            }
            Console.WriteLine("Done");
            System.Console.WriteLine();
        }
    }
}
