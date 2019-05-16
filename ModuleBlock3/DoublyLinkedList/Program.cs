using System;

namespace DoublyLinkedList
{
    internal class Program
    {
        private static void Main()
        {
            var doublyLinkedList = new DoublyLinkedList();
            doublyLinkedList.Add(5);
            doublyLinkedList.Add(10);
            doublyLinkedList.Add(15);
            Console.WriteLine(doublyLinkedList.Contains(10));
            Console.WriteLine(doublyLinkedList.Contains(25));
            Console.WriteLine(doublyLinkedList.Remove(10));
            Console.WriteLine(doublyLinkedList.FindByIndex(1));
            Console.WriteLine(doublyLinkedList.Count);
            Console.WriteLine(doublyLinkedList[1]);
            doublyLinkedList[1] = 20;
            Console.WriteLine(doublyLinkedList[1]);
            Console.Read();
        }
    }
}
