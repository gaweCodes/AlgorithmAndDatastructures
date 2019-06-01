namespace DoublyLinkedList
{
    public class DoublyLinkedList
    {
        private sealed class Node
        {
            public object Item { get; set; }
            public Node Next { get; set; }
            public Node Prev { get; set; }
        }
        private Node _startNode;
        private Node _endNode;
        public int Count { get; private set; }
        public void Add(object item)
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
        public bool InsertAfter(object previousItem, object newItem)
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
        public bool InsertBefore(object nextItem, object newItem)
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
        public bool Contains(object item) => Find(item) != null;
        public bool Remove(object item)
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
        private Node FindPrevious(object data)
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