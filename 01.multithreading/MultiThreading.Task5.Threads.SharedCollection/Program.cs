/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        const int itemsToAddCount = 10;
        private static readonly EventWaitHandle EventWaitHandle1 = new AutoResetEvent(false);
        private static readonly EventWaitHandle EventWaitHandle2 = new AutoResetEvent(false);
        private static readonly IList<int> _numbers = new List<int>();
        static void Main(string[] _)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            var writingThread = new Thread(WriteNumbersIntoCollection);
            var printingThread = new Thread(PrintNumbersFromCollection);
            writingThread.Start(itemsToAddCount);
            printingThread.Start(itemsToAddCount);

            Console.ReadLine();
        }

        private static void PrintNumbersFromCollection(object number)
        {
            for (var i = 0; i < (int)number; i++)
            {
                EventWaitHandle2.WaitOne();
                foreach (var n in _numbers)
                {
                    Console.WriteLine($"Number is {n}");
                }
                EventWaitHandle1.Set();

                Console.WriteLine();
            }
        }

        private static void WriteNumbersIntoCollection(object number)
        {
            for (var i = 0; i < (int)number; i++)
            {
                EventWaitHandle2.Set();
                _numbers.Add(i + 1);
                EventWaitHandle1.WaitOne();
            }
        }
    }
}
