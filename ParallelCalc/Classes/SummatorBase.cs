using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalc.Classes
{
    public class SummatorBase
    {
        protected string _name;
        public SummatorBase()
        {
            _name = "Последовательно";
        }

        public string IntroduceYourself() => $"Выбранный метод расчета: {_name}";

        virtual public long DoSumm(int[]? array, int threadsNumber)
        {
            var sum = 0L;
            if (array is null)
                return sum;

            foreach (var item in array)
                sum += item;
            return sum;
        }


        protected static void summarise(int[] valuesArray, int start, int end, int threadIndex, long[] threadSums)
        {
            var localSum = 0L;
            for (var j = start; j < end; j++)
                localSum += valuesArray[j];

            // Можно Interlocked.Add(ref partialSums[threadIndex], localSum);
            // но в этом нет особой надобности - каждый поток пищет в свой индекс массива.
            //
            threadSums[threadIndex] = localSum;
        }


    }
}
