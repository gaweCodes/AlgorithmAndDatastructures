using System;
using Stack;

namespace TasksStack
{
    internal static class StringExtension
    {
        public static double EvaluateExpression(this string expression)
        {
            var operations = new StackArrayList<string>();
            var values = new StackArrayList<double>();
            var tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in tokens)
            {
                if (t.IsNumeric())
                    values.Push(double.Parse(t));
                else
                    operations.Push(t);

                if (values.Count != 2) continue;
                var v2 = values.Pop();
                var v1 = values.Pop();
                switch (operations.Pop())
                {
                    case "+":
                        values.Push(v1 + v2);
                        break;
                    case "-":
                        values.Push(v1 - v2);
                        break;
                    case "*":
                        values.Push(v1 * v2);
                        break;
                    case "/":
                        values.Push(v1 / v2);
                        break;
                }
            }
            return values.Pop();
        }
        public static bool IsNumeric(this string value) => double.TryParse(value, out var d);
    }
}
