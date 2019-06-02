namespace ProblemInheritance
{
    internal class StringList : SinglyLinkedList.SinglyLinkedList
    {
        public int MyCount { get; private set; }
        public override void Add(object item)
        {
            MyCount++;
            base.Add(item);
        }
        public override void AddRange(object[] objects)
        {
            //MyCount += objects.Length;
            base.AddRange(objects);
        }
    }
}
