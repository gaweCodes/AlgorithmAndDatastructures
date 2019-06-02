using System;
using Stack;

namespace TasksStack
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine(FactorialRecursive(5));
            Console.WriteLine(FactorialIterative(5));

            string expression;
            Console.WriteLine("Eingabe eines arithmetischen Ausdrucks");
            Console.WriteLine("Auswertung von links nach rechts mit Leerzeichen");
            Console.WriteLine("z.B. 2 + 3 * 4, Ergebnis = 20");
            while (!string.IsNullOrEmpty(expression = Console.ReadLine())) Console.WriteLine("Ergebnis: " + expression.EvaluateExpression());
            Console.ReadLine();
        }
        public static int FactorialIterative(int n)
        {
            var stack = new StackSinglyLinkedList<int>();
            for (; n > 1; n--) stack.Push(n);
            while (stack.Count != 0) n *= stack.Pop();
            return n;
        }
        public static int FactorialRecursive(int n)
        {
            if (n == 0)
                return 1;
            return n * FactorialRecursive(n - 1);
        }
    }
}
