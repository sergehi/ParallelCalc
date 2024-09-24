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

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using ParallelCalc.Classes;
using ParallelCalc.Interfaces;

internal class Program
{
    static void Main(string[] args)
    {
        var numberOfThreads = Environment.ProcessorCount;
        var randomGenerator = new RangomLongGenerator();
        var sizes = new int[]{ 100_000, 1_000_000, 10_000_000 };

        WriteEnvronmentInfo();
        Console.WriteLine();
        Console.WriteLine($"Расчет:");
        var summators = new SummatorBase[] { new SummatorBase(), new SummatorParallelThread(), new SummatorParallelTask(), new SummatorParallelLINQ() };
        foreach (var size in sizes)
        {
            Console.WriteLine($"Размер массива: {size:N0} элементов");
            var valuesArray = randomGenerator.GenerateArray(size, 0, 1000);
            foreach (var summator in summators)
            {
                var stopwatch = Stopwatch.StartNew();
                var sum = summator.DoSumm(valuesArray, numberOfThreads);
                stopwatch.Stop();
                Console.WriteLine($"{summator.IntroduceYourself()}:\tСумма: {sum}\tВремя:{stopwatch.ElapsedMilliseconds} мс.");
            }
        }
        Console.ReadKey();
    }


    static void WriteEnvronmentInfo()
    {
        Console.WriteLine("Окружение:");
        Console.WriteLine($"Операционная система: {Environment.OSVersion}");
        Console.WriteLine($"Процессор: {Environment.ProcessorCount} логических процессоров");
    }
}