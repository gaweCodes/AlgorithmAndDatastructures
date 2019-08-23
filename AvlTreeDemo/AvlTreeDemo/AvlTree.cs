using System;

namespace AvlTreeDemo
{
    public enum TraverseModeEnum
    {
        PreOrder,
        PostOrder,
        InOrder,
        ReverseInOrder
    }
    public class AvlTree<T> where T : IComparable<T>
    {
        internal Node<T> root;
        public AvlTree()
        {
            TraverseMode = TraverseModeEnum.PreOrder;
        }
        public Node<T> Root => root;
        public int Count { get; private set; }
        public TraverseModeEnum TraverseMode { get; set; }
        public void Add(T item)
        {
            if (root == null)
                root = new Node<T>(item, null, this);
            else
                AddTo(root, item);
            Count++;
        }
        public void AddRange(T[] items)
        {
            foreach (var item in items) Add(item);
        }

        private void AddTo(Node<T> node, T item)
        {
            if (item.CompareTo(node.Item) < 0)
            {
                if (node.Left == null)
                    node.Left = new Node<T>(item, node, this);
                else
                    AddTo(node.Left, item);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new Node<T>(item, node, this);
                else
                    AddTo(node.Right, item);
            }
            node.Balance(item, false);
        }

        public bool Contains(T item) => Search(item) != null;
        public Node<T> Search(T item)
        {
            var node = root;
            while (node != null)
            {
                var c = item.CompareTo(node.Item);
                if (c == 0) return node;
                node = c < 0 ? node.Left : node.Right;
            }
            return null;
        }
        public void Clear()
        {
            root = null;
            Count = 0;
        }
        public override string ToString()
        {
            var s = "";
            const int level = 0;
            Traverse(root, level, ref s, null);
            return s;
        }
        private void Traverse(Node<T> node, int level, ref string s, Action<T> action)
        {
            if (node == null) return;
            var reverse = TraverseMode == TraverseModeEnum.ReverseInOrder;
            if (TraverseMode == TraverseModeEnum.PreOrder)
            {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Right : node.Left, level + 2, ref s, action);
            if (TraverseMode == TraverseModeEnum.InOrder || TraverseMode == TraverseModeEnum.ReverseInOrder)
            {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Left : node.Right, level + 2, ref s, action);
            if (TraverseMode == TraverseModeEnum.PostOrder)
            {
                s += "".PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
        }

        public void Traverse(TraverseModeEnum mode, Action<T> action)
        {
            var currentMode = TraverseMode;
            TraverseMode = mode;
            var s = "";
            var level = 0;
            Traverse(root, level, ref s, action);
            TraverseMode = currentMode;
        }

        public bool Remove(T value)
        {
            var current = Search(value);
            if (current == null) return false;
            var treeToBalance = current.Parent;
            Count--;
            if (current.Right != null || current.Left != null)
            {
                if (current.Right == null)
                {
                    if (current.Parent == null)
                    {
                        root = current.Left;
                        if (root != null) root.Parent = null;
                    }
                    else
                    {
                        var result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0)
                        {
                            current.Parent.Left = current.Left;
                            treeToBalance = current.Left;
                        }
                        else if (result > 0)
                        {
                            current.Parent.Right = current.Left;
                            treeToBalance = current.Left;
                        }
                    }
                }
                else if (current.Left == null)
                {
                    if (current.Parent == null)
                    {
                        root = current.Right;
                        if (root != null) root.Parent = null;
                    }
                    else
                    {
                        var result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0)
                        {
                            current.Parent.Left = current.Right;
                            treeToBalance = current.Right;
                        }
                        else if (result > 0)
                        {
                            current.Parent.Right = current.Right;
                            treeToBalance = current.Right;
                        }
                    }
                }
                else
                {
                    var leftmost = current.Right.Left;
                    if (leftmost == null)
                    {
                        leftmost = current.Right;
                        leftmost.Left = current.Left;
                    }
                    else
                    {
                        while (leftmost.Left != null) leftmost = leftmost.Left;
                        leftmost.Parent.Left = leftmost.Right;
                        leftmost.Left = current.Left;
                        leftmost.Right = current.Right;
                    }

                    if (current.Parent == null)
                    {
                        root = leftmost;
                        if (root != null) root.Parent = null;
                    }
                    else
                    {
                        var result = current.Item.CompareTo(current.Parent.Item);
                        if (result < 0)
                        {
                            current.Parent.Left = leftmost;
                            treeToBalance = leftmost;
                        }
                        else if (result > 0)
                        {
                            current.Parent.Right = leftmost;
                            treeToBalance = leftmost;
                        }
                    }
                }
            }

            if (treeToBalance != null)
                while (treeToBalance != null)
                {
                    var next = treeToBalance.Parent;
                    treeToBalance.Balance(value, true);
                    treeToBalance = next;
                }
            else
                root?.Balance(value, true);

            return true;
        }
    }
}