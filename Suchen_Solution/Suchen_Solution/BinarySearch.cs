using System;

namespace Suchen_Solution {
    public class BinarySearch :ISearch  {
        public SearchResult Find(int[] data, int value, bool unsorted) {
            var searchResult = new SearchResult();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            if (unsorted) {
                Array.Sort(data);
            }
            
            var start = 0;
            var end = data.Length - 1;
            var middle = (end - start) / 2;   //floor

            while (start < end) {
                if (data[middle] == value)
                    break;

                if (value > data[middle]) start = middle + 1;
                else end = middle - 1;
                middle = start + (end - start) / 2;
            }

            watch.Stop();
            searchResult.Ticks = watch.ElapsedTicks;
            if (data[middle] == value) searchResult.PositionFound = middle;
            return searchResult;
        }
    }
}
