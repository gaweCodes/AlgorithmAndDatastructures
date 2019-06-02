using System;

namespace Stack
{
    public class StackSinglyLinkedList<T> : ICollection
    {
        private readonly SinglyLinkedList<T> _items = new SinglyLinkedList<T>();
        public int Count => _items.Count;
        public void Push(T item) => _items.Add(item);
        public T Pop()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No items in stack");

            var item = _items[Count-1];
            _items.Remove(item);
            return item;
        }
        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("No items in stack");
            return _items[Count - 1];
        }
        public void Clear() => _items.Clear();
    }
}
