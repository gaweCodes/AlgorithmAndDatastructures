using System;
using System.Collections.Generic;
using System.Linq;

namespace Hashtable
{
    public class Hashtable<TKey, TValue>
    {
        private class Pair
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public Pair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
        private ArrayList<Pair>[] _items;
        public int Count { get; private set; }
        public double LoadFactor
        {
            get
            {
                var buckets = 0;
                foreach (var pairs in _items)
                {
                    if (pairs != null) buckets++;
                }
                return 1.0 - (_items.Length - buckets) / (double)_items.Length;
            }
        }
        public double OccupationFactor
        {
            get
            {
                var buckets = _items.Count(pairs => pairs != null);
                return (double)Count / buckets;
            }
        }
        public Hashtable(int length = 10)
        {
            length = CalcPrimeLength(length);
            _items = new ArrayList<Pair>[length];
        }
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Item already exist in collection.");
            var hash = GetHash(key);
            var list = _items[hash];
            if (list == null)
            {
                list = new ArrayList<Pair>();
                _items[hash] = list;
            }
            list.Add(new Pair(key, value));
            Count++;
        }
        public void Remove(TKey key)
        {
            var hash = GetHash(key);
            var list = _items[hash];
            if (list == null) return;
            for (var i = 0; i < list.Count; i++)
            {
                if (!list[i].Key.Equals(key)) continue;
                list.RemoveAt(i);
                Count--;
                break;
            }
        }
        public TValue this[TKey key]
        {
            get
            {
                var hash = GetHash(key);
                var list = _items[hash];
                if (list == null) return default;
                foreach (Pair t in list)
                {
                    if (t.Key.Equals(key)) return t.Value;
                }
                return default;
            }
            set
            {
                if (!ContainsKey(key))
                    Add(key, value);
                else
                    Update(key, value);
            }
        }
        public void Clear()
        {
            _items = _items = new ArrayList<Pair>[_items.Length];
            Count = 0;
        }
        public bool Contains(TKey key)
        {
            return ContainsKey(key);
        }
        public bool ContainsKey(TKey key)
        {
            var hash = GetHash(key);
            var list = _items[hash];
            if (list == null) return false;
            foreach (Pair t in list)
            {
                if (t.Key.Equals(key)) return true;
            }
            return false;
        }
        public IEnumerable<TValue> Values() => from list in _items where list != null from t in list select t.Value;
        public IEnumerable<TKey> Keys() => from list in _items where list != null from t in list select t.Key;
        public override string ToString()
        {
            var s = _items.Aggregate("", (current1, list) => list.Aggregate(current1, (current, pair) => current + pair.Key.ToString() + "|" + pair.Value.ToString() + " -> "));
            return s + "Count: " + Count;
        }
        private void Update(TKey key, TValue value)
        {
            var hash = GetHash(key);
            var list = _items[hash];
            if (list == null) return;
            foreach (Pair t in list)
            {
                if (!t.Key.Equals(key)) continue;
                t.Value = value;
                break;
            }
        }
        private int CalcPrimeLength(int length)
        {
            while (!IsPrime(++length)) { }
            return length;
        }
        private bool IsPrime(int number)
        {
            for (var i = 2; i <= number / 2; i++)
                if (number % i == 0) return false;
            return true;
        }
        private int GetHash(TKey key) => Math.Abs(key.GetHashCode()) % _items.Length;
    }
}
