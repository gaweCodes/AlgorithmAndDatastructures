using System;

namespace Suchen_Solution {
    static class Program {
        static void Main(string[] args) {
            SearchScreen();
        }

        static void SearchScreen() {
            Console.Clear();
            new ArraySearchUi(10000000, 54).Print();

            Console.WriteLine();
            Console.WriteLine("Press any key for home screen!");
            Console.ReadKey(true);
        }
    }

}
