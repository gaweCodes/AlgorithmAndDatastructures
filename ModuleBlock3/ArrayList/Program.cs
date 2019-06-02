using System;

namespace ArrayList
{
    internal class Program
    {
        private static void Main()
        {
            var list = new ArrayList<string>
            {
                "Muster",
                "Müller",
                "Schmidt",
                "Schulze",
                "Meier"
            };
            
            foreach (var entry in list) Console.WriteLine(entry);
            for (var i = list.Count - 1; i >= 0; i--) list.RemoveAt(i);
            Console.ReadLine();
        }
    }
}
