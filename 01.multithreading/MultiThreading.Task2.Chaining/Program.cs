/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        const int ArrayLength = 10;
        private static Random random = new Random();
        private static void Main(string[] _)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task<int[]> createRandomArrayTask = Task.Factory.StartNew(CreateArray);
            Task<int[]> multiplyRandomArrayTask = createRandomArrayTask.ContinueWith(numbers => MultiplyIntegersInArray(numbers.Result));
            Task<int[]> sortNumbersAscTask = multiplyRandomArrayTask.ContinueWith(numbers => SortNumbersByAsc(numbers.Result));
            sortNumbersAscTask.ContinueWith(numbers => CalculateAverage(numbers.Result));

            Console.ReadLine();
        }
    
        private static int[] CreateArray()
        {
            var numbers = new int[ArrayLength];
            for (var i = 0; i < ArrayLength; i++)
            {
                numbers[i] = random.Next(1, 1000);
            }

            PrintNumbers(numbers);

            return numbers;
        }

        private static int[] MultiplyIntegersInArray(int[] numbers)
        {
            var multiplier = random.Next(1, 1000);
            for (var i = 0; i < numbers.Length; i++)
            {
                numbers[i] = numbers[i] * multiplier;
            }

            PrintNumbers(numbers);

            return numbers;
        }

        private static int[] SortNumbersByAsc(int[] numbers)
        {
            Array.Sort(numbers);

            PrintNumbers(numbers);

            return numbers;
        }

        private static void CalculateAverage(int[] numbers)
        {
            var sum = 0;

            Array.ForEach(numbers, (n) => { sum += n; });

            PrintAverage(sum / numbers.Length);
        }

        private static void PrintNumbers(int[] numbers)
        {
            for (var i = 0; i <  numbers.Length; i++)
            {
                Console.WriteLine($"Number {i + 1} in array is {numbers[i]}");
            }

            Console.WriteLine();
        }

        private static void PrintAverage(int number)
        {
            Console.WriteLine($"Average number is {number}");
        }
    }
}
