using System;
using System.Diagnostics;
using System.Linq;
using SinglyLinkedListGeneric;
using ArrayList;

namespace ProfileMethods
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var count = 0;
            var argsCorrect = args.Length > 0 && int.TryParse(args[0], out count);
            if (!argsCorrect)
            {
                Console.WriteLine("Aufruf: ProfileMethods <AnzahlDurchläufe>\nBeispiel: ProfileMethods 10000");
                return;
            }
            var profiler = new Profiler(count);

            var arrayList = new ArrayList<int>();
            var linkedList = new SinglyLinkedListGeneric<int>();
            var arrayListSystem = new System.Collections.Generic.List<int>();
            var linkedListSystem = new System.Collections.Generic.LinkedList<int>();

            profiler.AddMethod(arrayList, "Add", true);
            profiler.AddMethod(arrayList, "Remove", true);
            profiler.AddMethod(linkedList, "Add", true);
            profiler.AddMethod(linkedList, "Remove", true);
            profiler.AddMethod(arrayListSystem, "Add", true);
            profiler.AddMethod(arrayListSystem, "Remove", true);
            profiler.AddMethod(linkedListSystem, "AddLast", true);
            profiler.AddMethod(linkedListSystem, "RemoveLast", true);
            profiler.AddMethod(arrayList, "Add", false);
            profiler.AddMethod(arrayList, "RemoveAt", true);
            profiler.AddMethod(arrayListSystem, "Add", false);
            profiler.AddMethod(arrayListSystem, "RemoveAt", true);

            //profiler.AddMethod(arrayList,        "Add",     false);
            //profiler.AddMethod(arrayList,        "IndexOf", true);
            //profiler.AddMethod(arrayListSystem,  "Add",     false);
            //profiler.AddMethod(arrayListSystem,  "IndexOf", true);
            //profiler.AddMethod(linkedList,       "Add",     false);
            //profiler.AddMethod(linkedList,       "Contains", true);
            //profiler.AddMethod(linkedListSystem, "AddLast",  false);
            //profiler.AddMethod(linkedListSystem, "Contains", true);

            profiler.Run();
            Console.ReadLine();
        }
        private class Profiler
        {
            readonly int _count;
            readonly int _countData;
            readonly Stopwatch _watch = new Stopwatch();
            readonly ArrayList<Method> _list = new ArrayList<Method>();
            private class Method
            {
                public object Obj { get; set; }
                public string Name { get; set; }
                public bool Protocol { get; set; }
            }
            public Profiler(int count)
            {
                _count = count;
                _countData = count / 1000;
                Console.WriteLine("Methode;Anzahl;Zeit(Sek.);Zeit(Ticks);Zeit pro Element(ms)");
            }
            public void AddMethod(object obj, string name, bool protocol)
            {
                _list.Add(new Method { Obj = obj, Name = name, Protocol = protocol });
            }
            public void Run()
            {
                foreach (var p in _list)
                {
                    var obj = p.Obj;
                    var type = obj.GetType();
                    var methods = type.GetMethods();
                    var method = methods.FirstOrDefault(methodInfo => methodInfo.Name.Equals(p.Name));
                    if (method == null)
                    {
                        Console.WriteLine(type + "." + p.Name + " existiert nicht!");
                        continue;
                    }

                    var countParams = method.GetParameters().Length;
                    _watch.Restart();
                    for (var n = _count - 1; n >= 0; n--)
                    {
                        method.Invoke(obj, countParams == 0 ? null : new object[] { n });
                        Show(type.Name + "." + method.Name, n, p.Protocol);
                    }
                    _watch.Stop();
                }
            }
            private void Show(string method, int n, bool protocol)
            {
                if ((_count - n) % _countData == 0 && protocol)
                    Console.WriteLine(method + ";" + (_count - n) + ";" + (_watch.ElapsedMilliseconds / 1000.0) + ";" + _watch.ElapsedTicks + ";" + (double)_watch.ElapsedMilliseconds / (_count - n));
            }
        }
    }
}
