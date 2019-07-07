using System;

namespace BinarySearchTree
{
    public enum TraverseModeEnum { PreOrder, PostOrder, InOrder, ReverseInOrder }
    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
        private Node _root;
        public int Count { get; private set; }
        public TKey Min
        {
            get
            {
                if (Count == 0)
                    throw new ArgumentException("No items in tree.");
                var node = _root;
                var min = _root.Key;
                while (node != null)
                {
                    min = node.Key;
                    node = node.Left;
                }
                return min;
            }
        }
        public TKey Max
        {
            get
            {
                if (Count == 0)
                    throw new ArgumentException("No items in tree.");
                var node = _root;
                var max = _root.Key;
                while (node != null)
                {
                    max = node.Key;
                    node = node.Right;
                }
                return max;
            }
        }
        public TraverseModeEnum TraverseMode { get; set; }
        public BinarySearchTree()
        {
            TraverseMode = TraverseModeEnum.PreOrder;
        }
        public void Add(TKey key, TValue value)
        {
            if (_root == null) _root = new Node {Key = key, Value = value};
            else AddTo(_root, key, value);
            Count++;
        }
        public void AddIterative(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new Node { Key = key, Value = value };
                return;
            }
            var node = _root;
            while (node != null)
            {
                if (key.CompareTo(node.Key) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = new Node() { Key = key, Value = value };
                        break;
                    }
                    node = node.Left;
                }
                else
                {
                    if (node.Right == null)
                    {
                        node.Right = new Node() { Key = key, Value = value };
                        break;
                    }
                    node = node.Right;
                }
            }

            Count++;
        }
        private void AddTo(Node node, TKey key, TValue value)
        {
            if (key.CompareTo(node.Key) < 0)
            {
                if (node.Left == null) node.Left = new Node { Key = key, Value = value };
                else AddTo(node.Left, key, value);
            }
            else
            {
                if (node.Right == null) node.Right = new Node { Key = key, Value = value };
                else AddTo(node.Right, key, value);
            }
        }
        public bool Contains(TKey key)
        {
            var node = _root;
            while (node != null)
            {
                var c = key.CompareTo(node.Key);
                if (c == 0) return true;
                node = c < 0 ? node.Left : node.Right;
            }
            return false;
        }
        public TValue this[TKey key]
        {
            get
            {
                Node node = _root;
                while (node != null)
                {
                    var c = key.CompareTo(node.Key);
                    if (c == 0) return node.Value;
                    node = c < 0 ? node.Left : node.Right;
                }
                throw new ArgumentException("Key does'nt exist.");
            }
        }
        public void Clear()
        {
            _root = null;
            Count = 0;
        }
        public override string ToString()
        {
            var s = "";
            const int level = 0;
            Traverse(_root, level, ref s);
            return s;
        }
        private void Traverse(Node node, int level, ref string s)
        {
            if (node == null) return;
            switch (TraverseMode)
            {
                case TraverseModeEnum.PreOrder:
                    s += string.Empty.PadLeft(level, ' ') + node.Key + "\n";
                    Traverse(node.Left, level + 2, ref s);
                    Traverse(node.Right, level + 2, ref s);
                    break;
                case TraverseModeEnum.PostOrder:
                    Traverse(node.Left, level + 2, ref s);
                    Traverse(node.Right, level + 2, ref s);
                    s += string.Empty.PadLeft(level, ' ') + node.Key.ToString() + "\n";
                    break;
                case TraverseModeEnum.InOrder:
                    Traverse(node.Left, level + 2, ref s);
                    s += string.Empty.PadLeft(level, ' ') + node.Key + "\n";
                    Traverse(node.Right, level + 2, ref s);
                    break;
                case TraverseModeEnum.ReverseInOrder:
                    Traverse(node.Right, level + 2, ref s);
                    s += "".PadLeft(level, ' ') + node.Key + "\n";
                    Traverse(node.Left, level + 2, ref s);
                    break;
            }
        }
    }
}
