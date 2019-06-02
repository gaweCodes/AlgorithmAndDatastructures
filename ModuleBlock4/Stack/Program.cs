using System;

namespace Stack
{
    internal class Program
    {
        private static void Main()
        {
            var stack = new StackArrayList<int>();
            for (var i = 1; i < 100000; i++) stack.Push(i);
            while (stack.Count > 0) Console.WriteLine(stack.Pop());
            Console.ReadLine();
        }
    }
}
