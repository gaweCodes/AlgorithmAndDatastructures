using System;

namespace SpellCheck
{
    internal class Program
    {
        private static void Main()
        {
            var spelling = new Spelling(new[]{"Hallo", "Welt"});
            var color = Console.ForegroundColor;
            var text = "Hallo Weltasdf";
            foreach (Word word in spelling.CheckText(text))
            {
                if (!word.IsCorrect)
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(word + " ");
                Console.ForegroundColor = color;
            }

            Console.ReadLine();
        }
    }
}
