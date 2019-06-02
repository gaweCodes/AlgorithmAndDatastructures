using System;

namespace ProblemInheritance
{
    internal class Program
    {
        private static void Main()
        {
            var list = new StringList();
            list.AddRange(new object[] { "Muster", "Schmidt", "Müller" });
            Console.WriteLine("Falsche  Anzahl: " + list.MyCount);
            Console.WriteLine("Korrekte Anzahl: " + list.Count);
            Console.ReadLine();
        }
    }
}
