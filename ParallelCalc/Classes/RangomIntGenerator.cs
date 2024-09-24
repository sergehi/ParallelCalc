using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParallelCalc.Interfaces;

namespace ParallelCalc.Classes
{
    internal class RangomIntGenerator : IRandomGenerator<int>
    {
        private Random _random = new Random();
        public int Next(int minVal, int maxVal)
        {
            return _random.Next(minVal, maxVal + 1);
        }
        public int[]? GenerateArray(int Count, int minVal, int maxVal)
        {
            try
            {
                int[] array = new int[Count];
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
