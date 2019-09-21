using System;
using System.Linq;

namespace Suchen_Solution {
    public class SortedAndUniformProvider : IArrayDataProvider {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int AvgValue { get; }
        public int RandomValue { get; }
        public int NotFoundValue { get; }
        public int[] Data { get; }

        /// <summary>
        /// Provides a list of sorted and uniformly distributed integerss
        /// </summary>
        /// <param name="size">ize of the list</param>
        public SortedAndUniformProvider(int size) {
            var d = Enumerable.Range(1, size).ToList();

            MinValue = d[0];
            MaxValue = d[size - 1];
            AvgValue = d.First(x => x > (MinValue + MaxValue) / 2);
            RandomValue = d[new Random((int)DateTime.Now.Ticks).Next(0, d.Count - 1)];
            NotFoundValue = d.Max() + 1;
            Data = d.ToArray();
        }

    }
}
