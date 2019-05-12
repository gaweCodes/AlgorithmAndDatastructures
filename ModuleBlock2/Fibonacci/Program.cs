using System;
using System.Diagnostics;

namespace Fibonacci
{
    public class Program
    {
        private static void Main()
        {
            var stopWatch = new Stopwatch();
            //Complexity O(2^n)
            stopWatch.Start();
            var result = FibonacciRecursive(40);
            stopWatch.Stop();
            Console.WriteLine($"The recursive fibonacci number of 40 is {result}. It duration was {stopWatch.ElapsedTicks} ticks");
            stopWatch.Reset();
            
            //Complexity O(n)
            stopWatch.Start();
            result  = FibonacciIterative(40);
            Console.WriteLine($"The iterative fibonacci number of 40 is {result}. It duration was {stopWatch.ElapsedTicks} ticks");
            stopWatch.Stop();
            Console.ReadLine();
        }

        public static long FibonacciIterative(long length)
        {
            var fibonacci = 0;
            var second = 1;
            for (var i = 0; i < length; i++)
            {
                var temp = fibonacci;
                fibonacci = second;
                second += temp;
            }
            return fibonacci;
        }

        public static long FibonacciRecursive(long length)
        {
            if (length <= 2)
                return 1;
            return FibonacciRecursive(length - 1) + FibonacciRecursive(length - 2);
        }
    }
}
