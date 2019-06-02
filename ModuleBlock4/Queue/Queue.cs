using System;
using System.Linq;
using Stack;

namespace Queue
{
    public class Queue<T> : ICollection
    {
        private T[] _items;
        public Queue(int length = 10)
        {
            _items = new T[length];
        }
        public void Enqueue(T item)
        {
            Grow();
            _items[Count] = item;
            Count++;
        }
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");
            return _items[0];
        }
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("No items in queue.");
            var item = _items[0];
            Array.Copy(_items, 1, _items, 0, Count - 1);
            _items[Count - 1] = default;
            Count--;
            return item;
        }
        public void Clear()
        {
            _items = new T[10];
            Count = 0;
        }
        public override string ToString() => _items.Aggregate("", (current, item) => current + item + " -> ") + "Count: " + Count;
        private void Grow()
        {
            if (_items.Length >= Count + 1)
                return;
            Array.Resize(ref _items, _items.Length * 2);
        }
        public int Count { get; private set; }
    }
}
