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
            Console.WriteLine("First example: Without interceptors");
            await RunFoo (new SampleClass());

            Console.WriteLine("Second example: With classic interceptor");
            await RunFoo (new SampleClass_genIntercepted(new SampleSyncTracingInterceptor()));

            Console.WriteLine("Third example: With async interceptor");
            await RunFoo (new SampleClass_genIntercepted(new AsyncToSyncInterceptorAdapter(new SampleAsyncTracingInterceptor())));
        }

        private static async Task RunFoo(SampleClass instance)
        {
            try
            {
                await instance.Foo(throwException: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Outermost runner caught exception!");
                Console.WriteLine(ex);
            }
            Console.WriteLine("Done");
            Console.WriteLine();
        }
    }
}
