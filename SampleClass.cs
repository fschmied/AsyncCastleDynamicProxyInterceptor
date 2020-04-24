using System;
using System.Threading.Tasks;

namespace AsyncInterceptor
{
    class SampleClass
    {
        public virtual async Task Foo(int value)
        {
            Console.WriteLine("Foo here, I see: " + value);

            await Task.Delay(TimeSpan.FromSeconds(2.0));

            Console.WriteLine("Foo again after the wait");

            if (value == 42)
            {
                Console.WriteLine("Foo throwing because of 42");    
                throw new InvalidOperationException("42 detected");
            }

            Console.WriteLine("Foo returning");
        }
    }
}
