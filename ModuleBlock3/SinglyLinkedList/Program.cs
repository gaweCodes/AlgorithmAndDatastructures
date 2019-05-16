using System;

namespace SinglyLinkedList
{
    internal class Program
    {
        private static void Main()
        {
            var singlyLinkedList = new SinglyLinkedList();
            singlyLinkedList.Add(5);
            singlyLinkedList.Add(13);
            singlyLinkedList.Add(2);
            Console.WriteLine(singlyLinkedList.Contains(13));
            Console.WriteLine(singlyLinkedList.Contains(25));
            Console.WriteLine(singlyLinkedList.Remove(13));
            Console.WriteLine(singlyLinkedList.FindByIndex(1));
            Console.WriteLine(singlyLinkedList.Count);
            Console.WriteLine(singlyLinkedList[1]);
            singlyLinkedList[1] = 20;
            Console.WriteLine(singlyLinkedList[1]);
            Console.Read();
        }
    }
}
