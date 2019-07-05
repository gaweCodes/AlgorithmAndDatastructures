using System;

namespace HashtableLinearProbing
{
    internal class Program
    {
        private static void Main()
        {
            var h = new HashtableLinearProbing<int, string>();
            for (var i = 1; i <= 21; i += 2)
                h.Add(i, i.ToString());
            h[1] = "Hinzufügen/Ändern ohne Erzeugen Ausnahme";
            for (var i = 1; i <= 10; i++)
                h.Add(i % 2 == 0 ? i : i - 1 + 100, i.ToString());
            Console.WriteLine("Gelöscht? " + h.Remove(0));
            Console.WriteLine("Vorhanden? " + h.ContainsKey(0));
            Console.WriteLine("Vorhanden? " + h.ContainsKey(1));
            Console.WriteLine("Gelöscht? " + h.Remove(1));
            Console.ReadLine();
            h.Clear();
        }
    }
}
