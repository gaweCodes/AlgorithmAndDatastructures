using System;

namespace Hashtable
{
    internal class Program
    {
        private static void Main()
        {
            var h = new Hashtable<int, string>(10000);
            var random = new Random();
            for (var i = 0; i <= 100000; i++)
            {
                var n = random.Next(1, 100000);
                h[n] = "Zahl=" + n;
            }

            //foreach (var value in h.Values()) Console.Write(value + " ");
            Console.WriteLine("Load factor: " + h.LoadFactor);
            Console.WriteLine("Occupation factor: " + h.OccupationFactor);
            Console.WriteLine("Anzahl: " + h.Count);
            Console.ReadLine();
            h.Clear();
        }
    }
}
