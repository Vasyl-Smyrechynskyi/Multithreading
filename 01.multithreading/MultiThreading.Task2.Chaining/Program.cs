/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int ArrayLength = 10;
        private static readonly Random random = new Random();
        private async static Task Main(string[] _)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            await Task.Run(CreateArray)
                .ContinueWith(numbers => MultiplyIntegersInArray(numbers.Result))
                .ContinueWith(numbers => SortNumbersByAsc(numbers.Result))
                .ContinueWith(numbers => CalculateAverage(numbers.Result));

            Console.ReadLine();
        }
    
        private static List<int> CreateArray()
        {
            var numbers = new List<int>();
            for (var i = 0; i < ArrayLength; i++)
            {
                numbers.Add(random.Next(1, 1000));
            }

            return PrintNumbers(numbers);
        }

        private static List<int> MultiplyIntegersInArray(List<int> numbers)
        {
            var multiplier = random.Next(1, 1000);
            var resultNumbers = numbers.ToList()
                .Select(n => n * multiplier)
                .ToList();

            return PrintNumbers(resultNumbers);
        }

        private static List<int> SortNumbersByAsc(List<int> numbers)
        {
            return PrintNumbers(numbers.OrderBy(n => n).ToList());
        }

        private static void CalculateAverage(List<int> numbers)
        {
            Console.WriteLine($"Average number is {(int)numbers.Average()}");
        }

        private static List<int> PrintNumbers(List<int> numbers)
        {
            numbers.ForEach(n => Console.WriteLine($"Number is {n}"));

            Console.WriteLine();

            return numbers;
        }
    }
}
