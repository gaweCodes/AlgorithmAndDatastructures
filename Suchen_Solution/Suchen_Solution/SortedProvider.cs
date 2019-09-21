using System;
using System.Collections.Generic;
using System.Linq;

namespace Suchen_Solution {
    public class SortedProvider : IArrayDataProvider {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int AvgValue { get; }
        public int RandomValue { get; }
        public int NotFoundValue { get; }
        public int[] Data { get; }

        /// <summary>
        /// Provides a list of sorted but not uniformely distributed data
        /// </summary>
        /// <param name="size">Size of the list</param>
        public SortedProvider(int size) {
            var d = new List<int>();
            for (var i = 0; i < size; i++) {

                if (i < size / 2)
                    d.Add(i);
                else
                    d.Add(int.MaxValue - i);
            }

            MinValue = d[0];
            MaxValue = d[size - 1];
            AvgValue = d.First(x => x > (MinValue + MaxValue) / 2);
            RandomValue = d[new Random((int)DateTime.Now.Ticks).Next(0, d.Count - 1)];
            NotFoundValue = d.Max() + 1;
            Data = d.ToArray();
        }

    }
}
