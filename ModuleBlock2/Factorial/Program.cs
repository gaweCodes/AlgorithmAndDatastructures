using System;

namespace Factorial
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(FactorialIterative(50));
            Console.WriteLine(FactorialRecursive(50));
            Console.Read();
        }
        private static long FactorialIterative(long n)
        {
            long factorialNumber = 1;
            for (var i = 1; i <= n; i++)
                factorialNumber *= i;
            return factorialNumber;
        }
        private static long FactorialRecursive(long n)
        {
            if (n == 1)
                return n;
            return FactorialRecursive(n - 1) * n;
        }
    }
}
