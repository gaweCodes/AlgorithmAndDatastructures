using System;

namespace HashtableDoubleHashing
{
    internal class Program
    {
        private static void Main()
        {
            var h = new HashtableDoubleHashing<int, string> {[1] = "Hinzufügen/Ändern ohne Erzeugen Ausnahme"};
            for (var i = 1; i <= 10; i++)
                h.Add(i % 2 == 0 ? i : i - 1 + 100, i.ToString());
            Console.WriteLine(h.ContainsKey(1));
            Console.WriteLine(h.Remove(1));
            Console.ReadLine();
            h.Clear();
        }
    }
}
