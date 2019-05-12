using System;
using System.Diagnostics;

namespace WordReverse
{
    internal class Program
    {
        private static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            var res = WordReverseRecursive("SARG");
            sw.Stop();
            Console.WriteLine($"Aus 'SARG' wird {res} in {sw.ElapsedTicks} ticks");

            sw.Reset();
            sw.Start();
            res = WordReverseIterative("SARG");
            sw.Stop();
            Console.WriteLine($"Aus 'SARG' wird {res} in {sw.ElapsedTicks} ticks");
            Console.ReadLine();
        }

        private static string WordReverseRecursive(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length == 1)
                return word;

            var firstCharacter = word.Substring(0, 1);
            return WordReverseRecursive(word.Substring(1)) + firstCharacter;
        }
        private static string WordReverseIterative(string word)
        {
            var res = string.Empty;
            foreach (var character in word)
                res = character + res;

            return res;
        }
    }
}
