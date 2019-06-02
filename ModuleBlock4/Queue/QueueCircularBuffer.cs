using System;
using Stack;

namespace Queue
{
    public class QueueCircularBuffer<T> : ICollection
    {
        private T[] _items;
        private int _first, _last;
        public QueueCircularBuffer(int length = 10)
        {
            _items = new T[length];
        }
        public int Count { get; private set; }
        public void Enqueue(T item)
        {
            if (Count == _items.Length) throw new ArgumentOutOfRangeException("No more space in cirular buffer queue.");
            _items[_last] = item;
            _last++;
            Count++;
            _last %= _items.Length;
        }
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("No items in queue.");
            return _items[_first];
        }
        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("No items in queue.");
            var item = _items[_first];
            _items[_first] = default;
            _first++;
            Count--;
            _first = _first % _items.Length;
            return item;
        }
        public void Clear()
        {
            _items = new T[10];
            _last = _first = 0;
            Count = 0;
        }
        public override string ToString()
        {
            var s = "";
            int i = _first;
            while (i != _last)
            {
                s += _items[i] + " -> ";
                i = i + 1 == _items.Length ? 0 : i + 1;
            }
            s += "Count: " + Count.ToString();
            return s;
        }
    }
}
