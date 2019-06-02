using System;
using SinglyLinkedListGeneric;

namespace TypeSafety
{
    internal class Program
    {
        private static void Main()
        {
            var list = new SinglyLinkedListGeneric<Person>();
            list.AddRange(new[]
            {
                new Person {FirstName = "Hans", LastName = "Muster"},
                new Person {FirstName = "Peter", LastName = "Schmidt"},
                new Person {FirstName = "Berta", LastName = "Müller"},
                new Person {FirstName = "Hermann", LastName = "Schulze"},
            });

            foreach (var p in list)
            {
                Console.WriteLine(p.FirstName + " " + p.LastName);
                if (p.LastName.StartsWith("M"))
                    list.Remove(p);
            }

            Console.ReadLine();
        }
    }
}
