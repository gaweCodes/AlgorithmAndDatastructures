﻿using System;
using System.Diagnostics;

namespace PerformanceTests
{
    internal class Program1
    {
        public static void Main1()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("\nStart FibonacciRecursive");
            var res = FibonacciRecursive(40);
            Console.WriteLine("FibonacciRecursive(40) = " + res);
            Console.WriteLine("Time elapsed: {0:0.0000}", stopWatch.ElapsedMilliseconds / 1000.0);

            stopWatch.Restart();
            Console.WriteLine("\nStart FibonacciIterative");
            res = Fibonacci(40);
            Console.WriteLine("Fibonacci(40) = " + res);
            Console.WriteLine("Time elapsed: {0:0.0000}", stopWatch.ElapsedMilliseconds / 1000.0);

            Console.ReadLine();
        }


        /// <summary>
        /// Fakultät iterativ
        /// </summary>
        public static int Factorial(int n)
        {
            var result = 1;

            for (var i = 1; i <= n; i++)
                result = result * i;

            return result;
        }

        public static int FactorialRecursive(int n)
        {
            if (n == 0)
                return 1;

            return n * FactorialRecursive(n - 1);
        }

        public static long FibonacciRecursive(long len)
        {
            if (len == 1 || len == 2)
            {
                return 1;
            }

            return FibonacciRecursive(len - 1) + FibonacciRecursive(len - 2);
        }

        public static long Fibonacci(long a)
        {
            var fibonacci = 0;
            var second = 1;
            for (var i = 0; i < a; i++)
            {
                var temp = fibonacci;
                fibonacci = second;
                second += temp;
            }

            return fibonacci;
        }
    }
}
