//Домашнее задание
//Многопоточный проект
//Цель:
//Применение разных способов распараллеливания задач и оценка оптимального способа реализации.
//Описание/Пошаговая инструкция выполнения домашнего задания:
//1.Напишите вычисление суммы элементов массива интов:
//Обычное
//Параллельное(для реализации использовать Thread, например List)
//Параллельное с помощью LINQ
//2. Замерьте время выполнения для 100 000, 1 000 000 и 10 000 000
//3. Укажите в таблице результаты замеров, указав:
//Окружение(характеристики компьютера и ОС)
//Время выполнения последовательного вычисления
//Время выполнения параллельного вычисления
//Время выполнения LINQ

//Пришлите в чат с преподавателем помимо ссылки на репозиторий номера своих строк в таблице.

using System.Collections.Generic;
using System.Diagnostics;
using ParallelCalc.Implementations;
using ParallelCalc.Interfaces;

internal class Program
{


    private static void Main(string[] args)
    {
        IRandomGenerator<int> randomGenerator = new RangomIntGenerator();

        int[] sizes = { 100_000, 1_000_000, 10_000_000 };
        Console.WriteLine("Размер массива\t|\tПоследовательное\t|\tПараллельное (Thread)\t|\tПараллельное (LINQ)");
        Console.WriteLine("");
        foreach (var size in sizes)
        {
            var valuesArray = randomGenerator.GenerateArray(size, 0, 1000);
            // Последовательное вычисление
            var stopwatch = Stopwatch.StartNew();
            long sumSequential = SumSequential(valuesArray);
            stopwatch.Stop();
            long timeSequential = stopwatch.ElapsedMilliseconds;

            // Параллельное вычисление с использованием Thread
            stopwatch.Restart();
            long sumParallelThread = SumParallelThread(valuesArray);
            stopwatch.Stop();
            long timeParallelThread = stopwatch.ElapsedMilliseconds;

            // Параллельное вычисление с использованием LINQ
            stopwatch.Restart();
            //long sumParallelLINQ = SumParallelLINQ(valuesArray, Environment.ProcessorCount);
            long sumParallelLINQ = SumParallelLINQ(valuesArray, 1);
            stopwatch.Stop();
            long timeParallelLINQ = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"{size,-15}\t|\t{sumSequential,-16}\t|\t{sumParallelThread,-20}\t|\t{sumParallelLINQ}");
            Console.WriteLine($"{"",-15}\t|\t{timeSequential,-16}мс\t|\t{timeParallelThread,-20} мс\t|\t{timeParallelLINQ} мс");
        }
        Console.ReadKey();

    }
    static long SumSequential(int[]? array)
    {
        long sum = 0;

        if (array is null)
            return sum;

        foreach (var item in array)
        {
            sum += item;
        }
        return sum;
    }

    static long SumParallelThread(int[]? array)
    {
        if (array is null)
            return 0;

        int numberOfThreads = Environment.ProcessorCount;
        int chunkSize = array.Length / numberOfThreads;
        long[] partialSums = new long[numberOfThreads];
        Thread[] threads = new Thread[numberOfThreads];

        
        for (int i = 0; i < numberOfThreads; i++)
        { 
            int index = i;
            int start = i * chunkSize;
            int end = (i == numberOfThreads - 1) ? array.Length : start + chunkSize;

            Action<int> summator = (x) =>
            {
                long localSum = 0;
                for (int j = start; j < end; j++)
                {
                    localSum += array[j];
                }
                partialSums[x] = localSum;
            };
            threads[i] = new Thread(() => summator(index));
            threads[i].Start();
        }
        foreach (var thread in threads)
        {
            thread.Join();
        }
        return partialSums.Sum();
    }

    static long SumParallelLINQ(int[]? array, int degreeOfParallelism)
    {
        if (array is null)
            return 0;

        return array.AsParallel().WithDegreeOfParallelism(degreeOfParallelism).Sum(x => (long)x);
    }





}