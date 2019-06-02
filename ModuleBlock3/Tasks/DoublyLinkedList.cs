using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private sealed class Node
        {
            public T Item { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
        }
        private Node _startNode;
        private Node _endNode;
        public int Count { get; private set; }
        public void Add(T item)
        {
            var newNode = new Node { Item = item };
            var last = _startNode;
            if (last == null)
            {
                _startNode = newNode;
                _endNode = newNode;
            }
            else
            {
                _endNode.Next = newNode;
                newNode.Prev = _endNode;
                _endNode = newNode;
            }
            Count++;
        }
        public void AddRange(T[] items)
        {
            foreach(var item in items) Add(item);
        }
        public bool InsertAfter(T previousItem, T newItem)
        {
            var previousNode = Find(previousItem);
            if (previousNode == null)
                return false;

            var newNode = new Node {Item = newItem, Next = previousNode.Next, Prev = previousNode};
            previousNode.Next = newNode;

            if (newNode.Next != null)
                newNode.Next.Prev = newNode;
            else
                _endNode = newNode;
            Count++;
            return true;
        }
        public bool InsertBefore(T nextItem, T newItem)
        {
            var nextNode = Find(nextItem);
            if (nextNode == null)
                return false;

            var newNode = new Node { Item = newItem, Prev = nextNode.Prev, Next = nextNode };
            nextNode.Prev = newNode;

            if (newNode.Prev != null)
                newNode.Prev.Next = newNode;
            else
                _startNode = newNode;
            Count++;
            return true;
        }
        private Node Find(T item)
        {
            var end = _startNode;
            while (end != null)
            {
                if (end.Item.Equals(item))
                    return end;
                end = end.Next;
            }
            return null;
        }
        public bool Contains(T item) => Find(item) != null;
        public bool Remove(T item)
        {
            var node = Find(item);
            if (node == null)
                return false;
            if (_startNode == node)
                _startNode = node.Next;
            
            if (_endNode == node)
                _endNode = node.Prev;

            if (node.Prev != null)
                node.Prev.Next = node.Next;          

            if (node.Next != null)
                node.Next.Prev = node.Prev;
            
            Count--;
            return true;
        }
        public bool RemoveAfter(T item)
        {
            var node = Find(item);
            if (node == null || node.Next == null)
                return false;
            
            //einfache Lösung, aber O(2n)
            //Remove(node.Next.Item);
            //return true;

            node = node.Next;
            if (_startNode == node)
                _startNode = node.Next;

            if (_endNode == node)
                _endNode = node.Prev;

            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            Count--;
            return true;
        }
        public bool Remove2(T item)
        {
            var node = Find(item);
            if (node == null)
                return false;
            var previousNode = node.Prev;
            if (previousNode != null)
            {
                previousNode.Next = node.Next;
                if (previousNode.Next != null) previousNode.Next.Prev = previousNode;
                if (node == _endNode) _endNode = previousNode;
            }
            else
            {
                _startNode = node.Next;
                if (_startNode == null)
                    _endNode = null;
                else
                    _startNode.Prev = null;
            }
            Count--;
            return true;
        }
        private Node FindPrevious(T data)
        {
            Node previousNode = null;
            var node = _startNode;
            while (node != null)
            {
                if (node.Item.Equals(data))
                    return previousNode;
                previousNode = node;
                node = node.Next;
            }
            return null;
        }
        public T FindByIndex(int index) => FindByIndexInternal(index).Item;
        private Node FindByIndexInternal(int index)
        {
            var node = _startNode;
            var i = 0;
            while (node != null)
            {
                if (i == index)
                    return node;
                i++;
                node = node.Next;
            }
            return null;
        }
        public T this[int index]
        {
            get => FindByIndexInternal(index).Item;
            set
            {
                var node = FindByIndexInternal(index);
                if (node != null)
                    node.Item = value;
            }
        }
        public void Clear()
        {
            _startNode = _endNode = null;
            Count = 0;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var node = _startNode;
            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
        public override string ToString()
        {
            var s = string.Empty;
            var node = _startNode;
            while (node != null)
            {
                s += node.Item + " -> ";
                node = node.Next;
            }
            s += "Count: " + Count.ToString();
            return s;
        }
        public bool InsertAt(int index, T item)
        {
            if (index < 0 || index >= Count)
                return false;

            var node = FindByIndexInternal(index);
            var newNode = new Node
            {
                Item = item,
                Prev = node.Prev,
                Next = node
            };

            if (node.Prev != null)
                node.Prev.Next = newNode;
            node.Prev = newNode;

            if (newNode.Prev == null)
                _startNode = newNode;

            Count++;
            return true;
        }
    }
}