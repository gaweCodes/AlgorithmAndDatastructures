using System;

namespace RedBlackTree
{
    internal class Program
    {
        private static void Main()
        {
            //var values = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            var values = new[] { "Hans", "Berta", "Andreas", "Kurt", "Nele", "Ken", "Marie", "Paul", "Peter", "Jo", "Zia" };

            //var tree = new RedBlackTree<int, string>();
            var tree = new RedBlackTree<string, string>();
            foreach (var value in values) tree.Add(value, value);
            Console.WriteLine(tree);
            Console.ReadLine();
        }
    }
}
