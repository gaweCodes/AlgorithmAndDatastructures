using System;

namespace Maximum
{
    internal class Program
    {
        private static void Main()
        {
            var s = new [] {8, 2, 6, 2, 0, 9};
            Console.WriteLine(MaximumRecursive(s, 0, s.Length - 1));
            Console.Read();
        }

        private static int MaximumRecursive(int[] s, int left, int right)
        {
            if (left == right)
                return s[left];
            var middle = (left + right) / 2.0;
            return Math.Max(MaximumRecursive(s, left, (int)Math.Floor(middle)), MaximumRecursive(s,  (int)Math.Ceiling(middle), right));
        }
    }
}
