using System;
using System.Collections.Generic;
using System.Linq;

namespace Suchen_Solution {
    public class UnsortedProvider : IArrayDataProvider {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int AvgValue { get; }
        public int RandomValue { get; }
        public int NotFoundValue { get; }
        public int[] Data { get; }

        /// <summary>
        /// Provides a list of random integers
        /// </summary>
        /// <param name="size">Size of the list</param>
        public UnsortedProvider(int size) {
            var d = new List<int>();
            var rnd = new Random(1);
            for (var i = 0; i < size; i++) {
                d.Add(rnd.Next(0, size * 10));
            }

            MinValue = d.Min();
            MaxValue = d.Max();
            AvgValue = d.OrderBy(x => x).First(x => x > (MinValue + MaxValue) / 2);
            RandomValue = d[new Random((int)DateTime.Now.Ticks).Next(0, d.Count - 1)];
            NotFoundValue = d.Max() + 1;
            Data = d.ToArray();
        }

    }
}
