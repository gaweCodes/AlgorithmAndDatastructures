using System;

namespace AvlTree
{
    public sealed class AvlTreeNode<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        public enum TreeState { Balanced, LeftHeavy, RightHeavy }
        private readonly AvlTree<TNode> _tree;
        private AvlTreeNode<TNode> _left;
        private AvlTreeNode<TNode> _right;
        public AvlTreeNode(TNode item, AvlTreeNode<TNode> parent, AvlTree<TNode> tree)
        {
            Item = item;
            Parent = parent;
            _tree = tree;
        }
        public TNode Item { get; set; }
        public AvlTreeNode<TNode> Left
        {
            get => _left;
            internal set
            {
                _left = value;
                if (_left != null) _left.Parent = this;
            }
        }
        public AvlTreeNode<TNode> Right
        {
            get => _right;
            internal set
            {
                _right = value;
                if (_right != null) _right.Parent = this;
            }
        }
        public AvlTreeNode<TNode> Parent { get; internal set; }
        private int LeftHeight => MaxChildHeight(Left);
        private int RightHeight => MaxChildHeight(Right);
        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1) return TreeState.LeftHeavy;
                if (RightHeight - LeftHeight > 1) return TreeState.RightHeavy;
                return TreeState.Balanced;
            }
        }
        private int BalanceFactor => RightHeight - LeftHeight;
        public int CompareTo(TNode other) => Item.CompareTo(other);
        private int MaxChildHeight(AvlTreeNode<TNode> node) => node != null ? 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right)) : 0;
        internal void Balance()
        {
            switch (State)
            {
                case TreeState.RightHeavy when Right != null && Right.BalanceFactor < 0:
                    RightLeftRotation();
                    break;
                case TreeState.RightHeavy:
                    LeftRotation();
                    break;
                case TreeState.LeftHeavy when Left != null && Left.BalanceFactor > 0:
                    LeftRightRotation();
                    break;
                case TreeState.LeftHeavy:
                    RightRotation();
                    break;
            }
        }
        private void LeftRotation()
        {
            var newRoot = Right;
            ReplaceRoot(newRoot);
            Right = newRoot.Left;
            newRoot.Left = this;
        }
        private void RightRotation()
        {
            var newRoot = Left;
            ReplaceRoot(newRoot);
            Left = newRoot.Right;
            newRoot.Right = this;
        }
        private void ReplaceRoot(AvlTreeNode<TNode> newRoot)
        {
            if (Parent != null)
            {
                if (Parent.Left == this) Parent.Left = newRoot;
                else if (Parent.Right == this) Parent.Right = newRoot;
            }
            else _tree.root = newRoot;
            newRoot.Parent = Parent;
            Parent = newRoot;
        }
        private void RightLeftRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }
        private void LeftRightRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }
    }
}
