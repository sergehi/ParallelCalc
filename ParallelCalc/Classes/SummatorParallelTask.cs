/// Суммирование с использованием Task 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalc.Classes
{
    internal class SummatorParallelTask : SummatorBase
    {
        public SummatorParallelTask()
        {
            _name = "Параллельно (Task)";
        }
        public override long DoSumm(long[]? array, int threadsNumber)
        { 
            return DoSummAsync(array, threadsNumber).GetAwaiter().GetResult();
        }


        async Task<long> DoSummAsync(long[]? array, int threadsNumber)
        {
            if (array is null)
                return 0;

            int chunkSize = array.Length / threadsNumber;
            var partialSums = new long[threadsNumber];
            var tasks = new Task[threadsNumber];

            for (int i = 0; i < threadsNumber; i++)
            {
                // отдельная переменная для параметра, что бы не передавать i, которая может перескочить на следующее значение к моменту начала выполения summator()
                var index = i;
                // Первый индекс чанка
                var start = i * chunkSize;
                // Последний индекс чанка. Финт ушами: для посделнего чанка end - это последний элемент в массиве
                var end = i == threadsNumber - 1 ? array.Length : start + chunkSize;

                tasks[i] = Task.Run(() => summarise(array, start, end, index, partialSums));
            }
            await Task.WhenAll(tasks);

            return partialSums.Sum();
        }
    }
}
