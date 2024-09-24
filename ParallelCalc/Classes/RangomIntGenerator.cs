using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelCalc.Interfaces;

namespace ParallelCalc.Classes
{
    internal class RangomIntGenerator : IRandomGenerator<long>
    {
        private Random _random = new Random();
        public long Next(long minVal, long maxVal)
        {
            return _random.Next((int)minVal, (int)maxVal + 1);
        }
        public long[]? GenerateArray(int Count, long minVal, long maxVal)
        {
            try
            {
                long[] array = new long[Count];
                for (int i = 0; i < Count; i++)
                    array[i] = Next(minVal, maxVal);
                return array;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при генерации массива случайных значений.\n{ex.ToString()}");
            }
            return null;
        }

    }
}
