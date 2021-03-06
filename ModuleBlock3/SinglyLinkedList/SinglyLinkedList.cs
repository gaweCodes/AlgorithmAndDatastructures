﻿namespace SinglyLinkedList
{
    public class SinglyLinkedList
    {
        private sealed class Node
        {
            public object Item { get; set; }
            public Node Next { get; set; }
        }
        private Node _startNode;
        private Node _endNode;
        public int Count { get; private set; }
        public virtual void Add(object item)
        {
            var newNode = new Node { Item = item };
            if (_startNode == null)
            {
                _startNode = newNode;
                _endNode = newNode;
            }
            else
            {
                _endNode.Next = newNode;
                _endNode = newNode;
            }
            Count++;
        }
        public virtual void AddRange(object[] items)
        {
            foreach (var item in items)
                Add(item);
        }
        private Node Find(object item)
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
        private Node FindPrev(object item)
        {
            var end = _startNode;
            Node prev = null;
            while (end != null)
            {
                if (end.Item.Equals(item))
                    return prev;
                prev = end;
                end = end.Next;
            }

            return null;
        }
        public bool Contains(object item) => Find(item) != null;
        public bool Remove(object item)
        {
            var node = Find(item);
            if (node == null)
                return false;

            var prev = FindPrev(item);
            if (prev == null)
                _startNode = node.Next;
            else
                prev.Next = node.Next;
            Count--;
            return true;
        }
        public object FindByIndex(int index) => FindByIndexInternal(index)?.Item;
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
        public object this[int index]
        {
            get => FindByIndexInternal(index)?.Item;
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
    }
}