using System;

namespace SumDifference
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(SumDifferenceIterative(1,5));
            Console.WriteLine(SumDifferenceRecursive(1, 5));
            Console.ReadLine();
        }
        private static int SumDifferenceIterative(int min, int max)
        {
            var sum = 0;
            while (min <= max)
            {
                sum += min;
                min++;
            }
            return sum;
        }
        private static int SumDifferenceRecursive(int min, int max, int result = 0)
        {
            if (min > max)
                return result;
            result += min;
            return SumDifferenceRecursive(++min, max, result);
        }
    }
}
