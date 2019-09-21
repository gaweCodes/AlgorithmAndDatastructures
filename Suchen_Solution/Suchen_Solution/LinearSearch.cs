namespace Suchen_Solution
{
    public class LinearSearch : ISearch
    {
        public SearchResult Find(int[] data, int value, bool unsorted)
        {
            var searchResult = new SearchResult ();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (var i = 0; i < data.Length; i++) {
                if (data[i] != value) continue;
                searchResult.PositionFound = i;
                break;
            }

            watch.Stop();
            searchResult.Ticks = watch.ElapsedTicks;
            return searchResult;
        }
    }
}
