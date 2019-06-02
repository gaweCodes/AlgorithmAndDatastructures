using System;
using System.Collections;
using System.Collections.Generic;

namespace Stack
{
    public class ArrayList<T> : IEnumerable<T>
    {
        protected T[] Items;
        public int Count { get; private set; }
        public ArrayList(int length = 4) => Items = new T[length];
        public void Add(T item)
        {
            Grow();
            Items[Count] = item;
            Count++;
        }
        public bool InsertAt(int index, T item)
        {
            if (index < 0 || index >= Count)
                return false;
            Grow();
            Array.Copy(Items, index, Items, index + 1, Count - index);
            Items[index] = item;
            Count++;
            return true;
        }
        public void AddRange(T[] items)
        {
            foreach (T item in items) Add(item);
        }
        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
                if (Items[i].Equals(item)) return i;
            return -1;
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            Array.Copy(Items, index + 1, Items, index, Count - (index + 1));
            Count--;
            Items[Count] = default;
        }
        public bool Remove(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (!Items[i].Equals(item)) continue;
                RemoveAt(i);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            Items = new T[4];
            Count = 0;
        }
        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                return Items[index];
            }
            set
            {
                if (index >= Count)
                    throw new IndexOutOfRangeException();
                Items[index] = value;
            }
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (var i = 0; i < Count; i++) yield return Items[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
        public override string ToString()
        {
            var s = string.Empty;
            for (var i = 0; i < Count; i++) s += Items[i] + " -> ";
            return s += "Count: " + Count;
        }
        private void Grow()
        {
            if (Items.Length >= Count + 1) return;
            var newLength = Items.Length * 2;
            Array.Resize(ref Items, newLength);
        }
    }
}