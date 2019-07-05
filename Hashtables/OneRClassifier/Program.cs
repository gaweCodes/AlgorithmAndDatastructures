using System;
using System.IO;
using System.Text;

namespace OneRClassifier
{
    internal class Program
    {
        private static void Main()
        {
            var oneR = new OneR();
            oneR.Build(new StreamReader("test.txt", Encoding.Default));
            Console.WriteLine("\nGültigen Wert zur Vorhersage eingeben (z.B. sonnig): ");
            string value;
            while ((value = Console.ReadLine()) != string.Empty)
            {
                var predicted = oneR.Classify(value);
                Console.WriteLine("Vorhersage: {0}", predicted ?? "<unbekannt>");
            }
        }
    }
}
