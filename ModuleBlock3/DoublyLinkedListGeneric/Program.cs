using System;

namespace DoublyLinkedListGeneric
{
    internal class Program
    {
        private static void Main()
        {
            var list = new DoublyLinkedList<string> {"Hans Muster", "Berta Müller", "Kurt Schmidt"};
            list.InsertAfter("Hans Muster", "Frieda Roth");
            Console.WriteLine(list);
            list.Remove2("Berta Müller");
            Console.WriteLine(list);
            list.Remove2("Hans Muster");
            Console.WriteLine(list);
            list.Remove2("Kurt Schmidt");
            Console.WriteLine(list);
            list.Clear();
            Console.ReadLine();
        }
    }
}
