using System.Collections.Generic;
using DoublyLinkedListGeneric;

namespace VisualStudioProfiler
{
    internal class Program
    {
        private static void Main()
        {
            var listSys = new LinkedList<int>();
            var listMy = new DoublyLinkedList<int>();

            for (var i = 0; i < 10000000; i++)
            {
                listSys.AddLast(i);
                listMy.Add(i);
            }
            for (var i = 0; i < 10000000; i++)
            {
                listSys.Remove(i);
                listMy.Remove(i);
            }
        }
    }
}
