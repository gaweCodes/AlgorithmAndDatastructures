using System;

namespace AvlTreeDemo
{
    public sealed class Node<TNode> : IComparable<TNode> where TNode : IComparable<TNode>
    {
        public enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy
        }
        private readonly AvlTree<TNode> _tree;
        private Node<TNode> _left;
        private Node<TNode> _right;
        public Node(TNode item, Node<TNode> parent, AvlTree<TNode> tree)
        {
            Item = item;
            Parent = parent;
            _tree = tree;
        }
        public TNode Item { get; set; }
        public Node<TNode> Left
        {
            get => _left;
            internal set
            {
                _left = value;
                if (_left != null) _left.Parent = this;
            }
        }
        public Node<TNode> Right
        {
            get => _right;
            internal set
            {
                _right = value;
                if (_right != null) _right.Parent = this;
            }
        }
        public Node<TNode> Parent { get; internal set; }
        private int LeftHeight => MaxChildHeight(Left);
        private int RightHeight => MaxChildHeight(Right);
        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                    return TreeState.LeftHeavy;

                if (RightHeight - LeftHeight > 1)
                    return TreeState.RightHeavy;

                return TreeState.Balanced;
            }
        }
        private int BalanceFactor => RightHeight - LeftHeight;
        public int CompareTo(TNode other) => Item.CompareTo(other);
        private int MaxChildHeight(Node<TNode> node) => node != null ? 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right)) : 0;
        internal void Balance<T>(T value, bool remove)
        {
            var action = remove ? "Löschen" : "Hinzufügen";
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    Console.WriteLine($"Beim {action} der Zahl {value} stelle ich beim Node {Item} eine Rechtslastigkeit fest, welche mit einer Right Left Rotation korrigiert (ausbalaniert) wird.");
                    RightLeftRotation();
                }
                else
                {
                    Console.WriteLine($"Beim {action} der Zahl {value} stelle ich beim Node {Item} eine Rechtslastigkeit fest, welche mit einer Left Rotation korrigiert (ausbalaniert) wird.");
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    Console.WriteLine($"Beim {action} der Zahl {value} stelle ich beim Node {Item} eine Linkslastigkeit fest, welche mit einer Left Right Rotation korrigiert (ausbalaniert) wird.");
                    LeftRightRotation();
                }
                else
                {
                    Console.WriteLine($"Beim {action} der Zahl {value} stelle ich beim Node {Item} eine Linkslastigkeit fest, welche mit einer Right Rotation korrigiert (ausbalaniert) wird.");
                    RightRotation();
                }
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
        private void ReplaceRoot(Node<TNode> newRoot)
        {
            if (Parent != null)
            {
                if (Parent.Left == this)
                    Parent.Left = newRoot;
                else if (Parent.Right == this) Parent.Right = newRoot;
            }
            else
                _tree.root = newRoot;
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