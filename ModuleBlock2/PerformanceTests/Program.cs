using System;
using System.Diagnostics;

namespace PerformanceTests
{
    internal class Program
    {
        private static readonly Stopwatch StopWatch = new Stopwatch();
        private static int _calls;
        private static void Main()
        {
            TestFibonacciIterative();
            TestFibonacciRecursive();
            TestTowerOfHanoi();

            ProcRec1(8);
            //_calls = 3 => Results: 4,2,1
            Console.WriteLine(_calls);

            _calls = 0;
            ProcRec1(128);
            //_calls = 7 => Results: 64, 62, 16, 8, 4, 2, 1
            Console.WriteLine(_calls);

            _calls = 0;
            ProcRec2(16);
            //1+2*(8+4+2+1) = 31
            Console.WriteLine(_calls);

            _calls = 0;
            ProcRec2(128);
            //1+2*(64+32+16+8+4+2+1) = 255
            Console.WriteLine(_calls);
            Console.Read();
        }
        private static void TestTowerOfHanoi()
        {
            for (var i = 0; i <= 15; i++)
            {
                StopWatch.Start();
                TowerOfHanoi.Program.MoveRingsRecursive(i, "A", "B", "C");
                StopWatch.Stop();
                Console.WriteLine(StopWatch.ElapsedTicks);
                StopWatch.Reset();
            }
        }
        private static void TestFibonacciIterative()
        {
            for (var i = 0; i <= 50000; i++)
            {
                StopWatch.Start();
                Fibonacci.Program.FibonacciIterative(i);
                StopWatch.Stop();
                Console.WriteLine(StopWatch.ElapsedTicks);
                StopWatch.Reset();
            }
        }
        private static void TestFibonacciRecursive()
        {
            for (var i = 10; i <= 40; i++)
            {
                StopWatch.Start();
                Fibonacci.Program.FibonacciRecursive(i);
                StopWatch.Stop();
                Console.WriteLine(StopWatch.ElapsedTicks);
                StopWatch.Reset();
            }
        }
        //Complexity O(log n)
        private static void ProcRec1(int n)
        {
            if (n <= 1)
                return;
            DoSomething();
            ProcRec1(n / 2);
        }
        //Complexity O(n)
        private static void ProcRec2(int n)
        {
            DoSomething();
            if (n <= 1)
                return;

            ProcRec2(n / 2);
            ProcRec2(n / 2);
        }
        private static void DoSomething()
        {
            _calls++;
        }
    }
}
