using System;

namespace AvlTree
{
    public enum TraverseModeEnum { PreOrder, PostOrder, InOrder, ReverseInOrder }
    public class AvlTree<T> where T : IComparable<T>
    {
        public AvlTree()
        {
            TraverseMode = TraverseModeEnum.PreOrder;
        }
        public AvlTreeNode<T> Root { get; internal set; }
        public int Count { get; private set; }
        public TraverseModeEnum TraverseMode { get; set; }
        public void Add(T item)
        {
            if (Root == null) Root = new AvlTreeNode<T>(item, null, this);
            else AddTo(Root, item);
            Count++;
        }
        public void AddRange(T[] items)
        {
            foreach (var item in items) Add(item);
        }
        private void AddTo(AvlTreeNode<T> node, T item)
        {
            if (item.CompareTo(node.Item) < 0)
            {
                if (node.Left == null)
                    node.Left = new AvlTreeNode<T>(item, node, this);
                else
                    AddTo(node.Left, item);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new AvlTreeNode<T>(item, node, this);
                else
                    AddTo(node.Right, item);
            }
            node.Balance();
        }
        public bool Contains(T item) => Search(item) != null;
        public AvlTreeNode<T> Search(T item)
        {
            var node = Root;
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
            Root = null;
            Count = 0;
        }
        public override string ToString()
        {
            var s = string.Empty;
            const int level = 0;
            Traverse(Root, level, ref s, null);
            return s;
        }
        private void Traverse(AvlTreeNode<T> node, int level, ref string s, Action<T> action)
        {
            if (node == null) return;
            var reverse = TraverseMode == TraverseModeEnum.ReverseInOrder;
            if (TraverseMode == TraverseModeEnum.PreOrder)
            {
                s += string.Empty.PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Right : node.Left, level + 2, ref s, action);
            if (TraverseMode == TraverseModeEnum.InOrder || TraverseMode == TraverseModeEnum.ReverseInOrder)
            {
                s += string.Empty.PadLeft(level, ' ') + node.Item + "\n";
                action?.Invoke(node.Item);
            }
            Traverse(reverse ? node.Left : node.Right, level + 2, ref s, action);
            if (TraverseMode != TraverseModeEnum.PostOrder) return;
            s += string.Empty.PadLeft(level, ' ') + node.Item + "\n";
            action?.Invoke(node.Item);
        }
        public void Traverse(TraverseModeEnum mode, Action<T> action)
        {
            var currentMode = TraverseMode;
            TraverseMode = mode;
            var s = "";
            var level = 0;
            Traverse(Root, level, ref s, action);
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
                        Root = current.Left;
                        if (Root != null) Root.Parent = null;
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
                        Root = current.Right;
                        if (Root != null) Root.Parent = null;
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
                        Root = leftmost;
                        if (Root != null) Root.Parent = null;
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
                    treeToBalance.Balance();
                    treeToBalance = next;
                }
            else
                Root?.Balance();
            return true;
        }
    }
}
