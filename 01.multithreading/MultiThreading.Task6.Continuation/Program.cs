/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var cancellation = new CancellationTokenSource();

            var mainTask = Task.Run(() =>
                {
                    Console.WriteLine($"Main task executed { Thread.CurrentThread.ManagedThreadId}");
                })
                .ContinueWith((task) =>
                {
                    Console.WriteLine("a.   Continuation task executed regardless of the result of the parent task" +
                        $" Thread id {Thread.CurrentThread.ManagedThreadId}. Task Id {task.Id}");
                    throw null;
                },
                continuationOptions: TaskContinuationOptions.None)
                .ContinueWith((task) =>
                 {
                     Console.WriteLine("b.  Continuation task executed when the parent task finished without success" +
                         $" Thread id {Thread.CurrentThread.ManagedThreadId}. Task Id {task.Id}");
                     throw null;
                 },
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted)
                .ContinueWith(continuationAction: (task) =>
                {
                    Console.WriteLine("c.   Continuation task executed when the parent task would be finished with fail and parent task thread should be reused for continuation" +
                        $" Thread id {Thread.CurrentThread.ManagedThreadId}. Task Id {task.Id}");
                    cancellation.Cancel();
                    cancellation.Token.ThrowIfCancellationRequested();
                },
                cancellationToken: cancellation.Token,
                continuationOptions: TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                scheduler: TaskScheduler.Current)
                .ContinueWith(continuationAction: (task) =>
                {
                    Console.WriteLine("d.    Continuation task executed outside of the thread pool when the parent task would be cancelled\n" +
                    $" Thread id {Thread.CurrentThread.ManagedThreadId}. Task Id {task.Id}");
                },
                continuationOptions: TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            Console.ReadLine();
        }
    }
}
