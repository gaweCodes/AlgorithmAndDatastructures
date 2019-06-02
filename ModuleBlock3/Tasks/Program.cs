using System;
using System.Collections.Generic;

namespace Tasks
{
    internal class Program
    {
        private static void Main()
        {
            var list = new ArrayList<int>();
            list.AddRange(new[] { 1, 23, 4, 12, 22, 42 });
            while (list.Count > 0) list.RemoveAt(0);
            list.AddRange(new[] { 1, 23, 4, 12, 22, 42 });
            var listCopy = new ArrayList<int>(list);
            foreach (var item in listCopy) Console.WriteLine(item);

            // 4) null muss explizit in den Methoden berücksichtigt werden
            // 5) int kann kein null sein. Lösung: new ArrayList<int?>()

            var doubleList = new DoublyLinkedList<int>();
            doubleList.AddRange(new[] { 1, 23, 4, 12, 22, 42 });

            doubleList.RemoveAfter(23);

            foreach (double d in Pow(2, 16)) Console.WriteLine(d);

            list.InsertAt(1, 3);
            list.InsertAt(6, 99);
            list.InsertAt(0, 0);
            doubleList.InsertAt(2, 4);
            doubleList.InsertAt(1, 3);
            doubleList.InsertAt(6, 99);
            doubleList.InsertAt(0, 0);

            // 10)
            // ArrayList: O(1), O(1) im Average Case (Array muss nicht vergrößert werden)
            //            bei Umkopieren Array im Worst Case O(n), wenn an Position 0 eingefügt wird
            // LinkedList: O(n) im Worst/Average Case

            // 11)
            // Die Elemente werden zweimal iteriert, einmal zum Finden des Knotenelements und 
            // ein weiteres mal zum Finden des vorherigen Elements, also O(2n). Beim Suchen des Elementes in der 
            // Find-Methode sollte bereits das vorherige Element gemerkt werden, dann O(n). 
            // Alternative: Doppelt verkettete Liste.

            // 12) siehe Projekt "2 Datenstrukturen\2.2 List\7) VisualStudioProfiler"*/
            Console.ReadLine();
        }
        private static IEnumerable<double> Pow(double a, int n)
        {
            for (var i = 1.0; i <= n; i++) yield return Math.Pow(a, i);
        }
    }
}
