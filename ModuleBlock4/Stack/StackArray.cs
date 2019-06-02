﻿using System;
using System.Linq;

namespace Stack
{
    internal class StackArray<T> : ICollection
    {
        private T[] _items;
        public StackArray(int length = 10)
        {
            _items = new T[length];
        }
        public int Count { get; private set; }
        public void Push(T item)
        {
            Grow();
            _items[Count] = item;
            Count++;
        }
        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");

            var result = _items[--Count];
            _items[Count] = default;
            return result;
        }
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items on stack.");
            return _items[Count-1];
        }
        public void Clear()
        {
            _items = new T[10];
            Count = 0;
        }
        public override string ToString() => _items.Aggregate("", (current, item) => current + (item + " -> ")) + "Count: " + Count;
        private void Grow()
        {
            if (_items.Length >= Count + 1)
                return;
            Array.Resize(ref _items, _items.Length * 2);
        }
    }
}
