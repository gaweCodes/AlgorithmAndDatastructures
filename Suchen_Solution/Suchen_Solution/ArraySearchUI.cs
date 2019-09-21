using System;

namespace Suchen_Solution
{
    public class ArraySearchUi
    {

        private readonly int _datasize;
        public ArraySearchUi(int datasize, int tableWith)
        {
            _datasize = datasize;
            _tableWidth = tableWith;
        }
        public void Print()
        {
            PrintLine();
            PrintRow("Ticks needed to search an item in " + _datasize + " integers");
            PrintLine();

            PrintLine();
            PrintRow("", "Linear", "Binary");
            PrintLine();

            PrintRow("Sorted, uniform distribution array");
            PrintLine();
            IArrayDataProvider dataProvider = new SortedAndUniformProvider(_datasize);
            PrintGrid(
                Search(new LinearSearch(), dataProvider, false),
                Search(new BinarySearch(), dataProvider, false)
            );

            PrintRow("Sorted, not evenly distributed array");
            PrintLine();
            dataProvider = new SortedProvider(_datasize);
            PrintGrid(
                Search(new LinearSearch(), dataProvider, false),
                Search(new BinarySearch(), dataProvider, false)
            );

            PrintRow("Unsorted (random) array");
            PrintLine();
            dataProvider = new UnsortedProvider(_datasize);
            PrintGrid(
                Search(new LinearSearch(), dataProvider, true),
                Search(new BinarySearch(), dataProvider, true)
            );

            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Target found, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Target not found!");
            Console.WriteLine();
            Console.ForegroundColor = color;
        }

        private SearchResults Search(ISearch algorithm, IArrayDataProvider provider, bool sort) {

            var searchResults = new SearchResults {
                ArrayCount = provider.Data.Length,
                MinValue = algorithm.Find(provider.Data, provider.MinValue, sort),
                AvgValue = algorithm.Find(provider.Data, provider.AvgValue, sort),
                MaxValue = algorithm.Find(provider.Data, provider.MaxValue, sort),
                RandomValue = algorithm.Find(provider.Data, provider.RandomValue, sort),
                NotFoundValue = algorithm.Find(provider.Data, provider.NotFoundValue, sort)
            };


            return searchResults;
        }

        private void PrintGrid(SearchResults l, SearchResults b) {

            PrintRow("Min",
                (l.MinValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + l.MinValue.Ticks.ToString("00000000"),
                (b.MinValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + b.MinValue.Ticks.ToString("00000000")
            );
            PrintRow("Avg",
                (l.AvgValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + l.AvgValue.Ticks.ToString("00000000"),
                (b.AvgValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + b.AvgValue.Ticks.ToString("00000000")
            );
            PrintRow("Max",
                (l.MaxValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + l.MaxValue.Ticks.ToString("00000000"),
                (b.MaxValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + b.MaxValue.Ticks.ToString("00000000")
            );
            PrintRow("NotFound",
                (l.NotFoundValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + l.NotFoundValue.Ticks.ToString("00000000"),
                (b.NotFoundValue.PositionFound != null ? ConsoleColor.Blue : ConsoleColor.Red) + "|" + b.NotFoundValue.Ticks.ToString("00000000")
            );
            PrintLine();
        }

        readonly int _tableWidth;
        readonly ConsoleColor _defaultColor = Console.ForegroundColor;

        private void PrintLine() {
            Console.WriteLine(new string('-', _tableWidth+1));
        }

        private void PrintRow(params string[] columns) {
            var width = (_tableWidth - columns.Length) / columns.Length;
            Console.Write("|");

            foreach (var column in columns) {
                var color = _defaultColor;
                var text = column;
                if (column.Contains("|")) {
                    color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), column.Split('|')[0]);
                    text = column.Split('|')[1];
                }
                Console.ForegroundColor = color;
                Console.Write(AlignCentre(text, width));
                Console.ForegroundColor = _defaultColor;
                Console.Write("|");
            }

            Console.WriteLine();
        }

        private string AlignCentre(string text, int width) {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            return string.IsNullOrEmpty(text) ? new string(' ', width) : text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
