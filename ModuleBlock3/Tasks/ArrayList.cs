using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class ArrayList<T> : IEnumerable<T> where T : IComparable<T>
    {
        protected T[] Items;
        public int Count { get; private set; }
        public ArrayList(int length = 4) => Items = new T[length];
        public ArrayList(ArrayList<T> arrayList) : this()
        {
            foreach (var entry in arrayList) Add(entry);
        }
        public void Add(T item)
        {
            Grow();
            Items[Count] = item;
            Count++;
        }
        public void AddRange(T[] items)
        {
            foreach (T item in items) Add(item);
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
            if (Count < Items.Length / 2 && Items.Length > 4)
            {
                var newLength = Items.Length / 2;
                Array.Resize(ref Items, newLength);
            }
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
        public T Min
        {
            get
            {
                if (Count == 0) throw new InvalidOperationException("No data in list.");
                var min = this[0];
                for (var i = 1; i < Count; i++)
                    if (this[i].CompareTo(min) < 0) min = this[i];
                return min;

            }
        }
        public T Max
        {
            get
            {
                if (Count == 0) throw new InvalidOperationException("No data in list.");
                var max = this[0];
                for (var i = 1; i < Count; i++)
                    if (this[i].CompareTo(max) > 0) max = this[i];
                return max;

            }
        }
    }
}