using System;

namespace TowerOfHanoi
{
    public class Program
    {
        private static void Main()
        {
            //Complexity O(2^n)
            MoveRingsRecursive(3, "A", "B", "C");
            Console.ReadLine();
        }
        public static void MoveRingsRecursive(int rings, string source, string help, string target)
        {
            if (rings == 1)
                Console.WriteLine($"Ring moved from {source} to {target}");
            else if(rings > 1)
            {
                MoveRingsRecursive(rings - 1, source, target, help);
                MoveRingsRecursive(rings - 1, help, source, target);
            }
            else
                Console.WriteLine("Keine Scheiben vorhanden.");
        }
    }
}