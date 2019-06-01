using System;

namespace IndexOf
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(IndexOfIterative('S', "HAUS"));
            Console.WriteLine(IndexOfIterative('K', "HAUS"));
            Console.WriteLine(IndexOfRecursive('S', "HAUS"));
            Console.WriteLine(IndexOfRecursive('K', "HAUS"));
            Console.ReadLine();
        }
        private static int IndexOfIterative(char needle, string haystack)
        {
            for (var index = 0; index < haystack.Length; index++)
            {
                if (haystack[index] == needle)
                    return index;
            }
            return -1;
        }
        private static int IndexOfRecursive(char needle, string haystack)
        {
            var length = haystack.Length;
            if (length == 0)
                return -1;

            if (haystack[length - 1] == needle)
                return length - 1;
            return IndexOfRecursive(needle, haystack.Substring(0, length - 1));
        }
    }
}
