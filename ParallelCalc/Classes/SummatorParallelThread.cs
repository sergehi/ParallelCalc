// Суммирование с использованием Thread 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalc.Classes
{
    internal class SummatorParallelThread : SummatorBase
    {
        public SummatorParallelThread()
        {
            _name = "Параллельно (Thread)";
        }

        // Массив делится на диапазоны(chunks) и, затем, чанки вычисляются параллельно - для каждого создается Thread
        // Количество диапазонов получаем из числа логических процессоров, доступных для использования средой CLR (Environment.ProcessorCount)
        override public long DoSumm(long[]? array, int threadsNumber)
        {
            if (array is null)
                return 0;

            int chunkSize = array.Length / threadsNumber;
            var partialSums = new long[threadsNumber];
            var threads = new Thread[threadsNumber];

            for (int i = 0; i < threadsNumber; i++)
            {
                // отдельная переменная для параметра, что бы не передавать i, которая может перескочить на следующее значение к моменту начала выполения summator()
                var index = i;
                // Первый индекс чанка
                var start = i * chunkSize;
                // Последний индекс чанка. Финт ушами: для посделнего чанка end - это последний элемент в массиве
                var end = (i == threadsNumber - 1) ? array.Length : start + chunkSize;

                threads[i] = new Thread(() => summarise(array, start, end, index, partialSums));
                threads[i].Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            return partialSums.Sum();
        }

    }
}
