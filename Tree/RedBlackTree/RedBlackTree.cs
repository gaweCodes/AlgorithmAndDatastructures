using System;

namespace RedBlackTree
{
    // [Serializable()]
    public class RedBlackTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        // [Serializable()]
        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public bool IsRed { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
        }
        private Node _root;
        public int Count { get; private set; }
        public void Add(TKey key, TValue value)
        {
            _root = AddTo(_root, key, value);
            _root.IsRed = false;
            Count++;
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
                var node = _root;
                while (node != null)
                {
                    var c = key.CompareTo(node.Key);
                    if (c == 0) return node.Value;
                    node = c < 0 ? node.Left : node.Right;
                }
                throw new ArgumentException("Key does'nt exist.");
            }
        }
        private Node AddTo(Node node, TKey key, TValue value)
        {
            if (node == null) return new Node { Key = key, Value = value, IsRed = true };
            var compareResult = key.CompareTo(node.Key);
            if (compareResult < 0) node.Left = AddTo(node.Left, key, value);
            else if (compareResult > 0) node.Right = AddTo(node.Right, key, value);
            else node.Value = value;
            if (IsRed(node.Right) && !IsRed(node.Left)) node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right)) FlipColors(node);
            return node;
        }
        private static Node RotateRight(Node node)
        {
            var newParent = node.Left;
            node.Left = newParent.Right;
            newParent.Right = node;
            newParent.IsRed = newParent.Right.IsRed;
            newParent.Right.IsRed = true;
            return newParent;
        }
        private static Node RotateLeft(Node node)
        {
            var newParent = node.Right;
            node.Right = newParent.Left;
            newParent.Left = node;
            newParent.IsRed = newParent.Left.IsRed;
            newParent.Left.IsRed = true;
            return newParent;
        }
        private static void FlipColors(Node node)
        {
            node.IsRed = !node.IsRed;
            node.Left.IsRed = !node.IsRed;
            node.Right.IsRed = !node.IsRed;
        }
        private static bool IsRed(Node node) => node != null && node.IsRed;
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
            var color = node.IsRed ? "red" : "black";
            s += "".PadLeft(level, ' ') + $"{node.Key} ({color})\n";
            Traverse(node.Left, level + 2, ref s);
            Traverse(node.Right, level + 2, ref s);
        }
    }
}
