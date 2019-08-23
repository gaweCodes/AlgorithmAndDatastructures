using System;

namespace AvlTreeDemo
{
    internal class Program
    {
        private static void Main()
        {
            var numbers = new [] { 67, 6, 4, 80, 55, 40, 58, 48, 2, 50, 36, 49 };
            var tree = new AvlTree<int>();
            tree.AddRange(numbers);
            Console.ReadLine();
        }
    }
}
