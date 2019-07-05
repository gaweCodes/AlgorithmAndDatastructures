using System;
using System.Linq;

namespace HashtableLinearProbing
{
    public class HashtableLinearProbing<TKey, TValue>
    {
        private class Pair
        {
            public TKey Key { get; }
            public TValue Value { get; set; }
            public bool IsDeleted { get; internal set; }
            public Pair(TKey key, TValue value, bool isDeleted = false)
            {
                Key = key;
                Value = value;
                IsDeleted = isDeleted;
            }
        }
        private Pair[] _items;
        public int Count { get; private set; }
        public HashtableLinearProbing(int length = 10)
        {
            length = CalcPrimeLength(length * 2);
            _items = new Pair[length];
        }
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException("Item already exist in collection.");

            if (Count + 1 == _items.Length)
                throw new ArgumentException("No more space in array.");

            var hash = Math.Abs(key.GetHashCode()) % _items.Length;
            while (_items[hash] != null && !_items[hash].IsDeleted)
            {
                hash++;
                hash %= _items.Length;
            }
            _items[hash] = new Pair(key, value);
            Count++;
        }
        public bool Remove(TKey key)
        {
            var hash = Math.Abs(key.GetHashCode()) % _items.Length;
            while (_items[hash] != null)
            {
                if (_items[hash].Key.Equals(key) && !_items[hash].IsDeleted)
                {
                    _items[hash].IsDeleted = true;
                    Count--;
                    return true;
                }
                hash++;
                hash %= _items.Length;
            }
            return false;
        }
        public TValue this[TKey key]
        {
            get
            {
                var hash = Math.Abs(key.GetHashCode()) % _items.Length;
                while (_items[hash] != null)
                {
                    if (_items[hash].Key.Equals(key) && !_items[hash].IsDeleted)
                        return _items[hash].Value;
                    hash++;
                    hash %= _items.Length;
                }
                throw new ArgumentException("Key not found.");
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
            _items = new Pair[_items.Length];
            Count = 0;
        }
        public bool Contains(TKey key)
        {
            return ContainsKey(key);
        }
        public bool ContainsKey(TKey key)
        {
            var hash = Math.Abs(key.GetHashCode()) % _items.Length;
            while (_items[hash] != null && !_items[hash].IsDeleted)
            {
                if (_items[hash].Key.Equals(key))
                    return true;
                hash++;
                hash %= _items.Length;
            }
            return false;
        }
        public override string ToString()
        {
            var s = _items.Where(t => t != null && !t.IsDeleted).Aggregate("", (current, t) => current + t.Key.ToString() + "|" + t.Value.ToString() + " -> ");
            return s + "Count: " + Count;
        }
        private void Update(TKey key, TValue value)
        {
            var hash = Math.Abs(key.GetHashCode()) % _items.Length;
            while (_items[hash] != null && !_items[hash].IsDeleted)
            {
                if (_items[hash].Key.Equals(key))
                {
                    _items[hash].Value = value;
                    break;
                }
                hash++;
                hash %= _items.Length;
            }
        }
        private int CalcPrimeLength(int length)
        {
            while (!IsPrime(++length))
            {
            }
            return length;
        }
        private bool IsPrime(int number)
        {
            for (var i = 2; i <= number / 2; i++)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}