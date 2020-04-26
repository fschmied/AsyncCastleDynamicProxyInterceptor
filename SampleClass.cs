using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    public class SampleClass
    {
        public virtual async Task Foo(bool throwException)
        {
            Console.WriteLine("Foo here, I see: " + throwException);
            Console.WriteLine($"Foo will delay asynchronously a bit (thread before: {Thread.CurrentThread.ManagedThreadId})");

            await Task.Delay(TimeSpan.FromSeconds(2.0));

            Console.WriteLine($"Foo again after the delay (thread after: {Thread.CurrentThread.ManagedThreadId})");

            if (throwException)
            {
                Console.WriteLine("Foo throwing exception as instructed");    
                throw new InvalidOperationException("Exception requested");
            }

            Console.WriteLine("Foo returning");
        }
    }
}
