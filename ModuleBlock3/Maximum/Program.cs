using System;
using System.Collections.Generic;

namespace Maximum
{ 
    internal class Program
    {
        private static int _count;
        private static void Main()
        {
            var s = new int[21];
            var r = new Random();
            for (var index = 0; index < s.Length; index++)
                s[index] = r.Next(int.MinValue,int.MaxValue);

            Console.WriteLine($"Total {MaximumRecursive(s, 0, s.Length - 1)}");
            Console.WriteLine(_count);
            _count = 0;
            Console.WriteLine($"Total {MaximumIterative(s)}");
            MaximumIterative(s);
            Console.WriteLine(_count);
            Console.Read();
        }

        private static int MaximumIterative(IReadOnlyList<int> s)
        {
            var max = s[0];
            foreach (var number in s)
            {
                max = Math.Max(max, number);
                _count++;
            }
            return max;
        }
        private static int MaximumRecursive(IReadOnlyList<int> s, int left, int right)
        {
            _count++;
            if (left == right)
                return s[left];
            var middle = (left + right) / 2.0;
            return Math.Max(MaximumRecursive(s, left, (int)Math.Floor(middle)), MaximumRecursive(s,  (int)Math.Ceiling(middle), right));
        }
    }
}
