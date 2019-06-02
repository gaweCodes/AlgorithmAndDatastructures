﻿using System;

namespace Queue
{
    internal class Program
    {
        private static void Main()
        {
            var queue = new QueueCircularBuffer<int>();
            //var queue = new Queue<int>();
            for (var i = 1; i < 10; i++) queue.Enqueue(i);
            while (queue.Count > 0) Console.WriteLine(queue.Dequeue());
            for (var i = 1; i < 6; i++) queue.Enqueue(i);
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            Console.WriteLine(queue.Dequeue());
            queue.Enqueue(100);
            Console.WriteLine(queue.Dequeue());
            queue.Enqueue(101);
            Console.WriteLine(queue.Dequeue());
            while (queue.Count > 0) Console.WriteLine(queue.Dequeue());
            Console.ReadLine();
        }
    }
}
