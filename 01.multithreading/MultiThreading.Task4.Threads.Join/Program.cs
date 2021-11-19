/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        const int threadsCount = 10;
        private static readonly object numberLock = new object();
        private static Semaphore _semaphore = new Semaphore(5, 5);

        static void Main(string[] _)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            // a - implementation
            CreateWithThreadClassUsage(threadsCount);

            // b - implementation
            CreateWithThreadPoolClassUsage(threadsCount);

            Console.ReadLine();
        }

        private static void CreateWithThreadPoolClassUsage(object number)
        {
            var count = (int)number;
            if (count < 1)
                return;

            _semaphore.WaitOne();
            //lock (numberLock)
            //{
                Console.WriteLine($"b) Thread id is {Thread.CurrentThread.ManagedThreadId}. Number is {--count}");
                ThreadPool.QueueUserWorkItem(new WaitCallback(CreateWithThreadPoolClassUsage), count);
            //}
            _semaphore.Release();
        }

        private static void CreateWithThreadClassUsage(object number)
        {
            var count = (int)number;
            if (count < 1)
                return;

            Console.WriteLine($"a) Thread id is {Thread.CurrentThread.ManagedThreadId}. Number is {--count}");
            var thread = new Thread(CreateWithThreadClassUsage);
            thread.Start(count);
            thread.Join();
        }
    }
}
