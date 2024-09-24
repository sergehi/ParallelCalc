// Суммирование с использованием LINQ
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalc.Classes
{
    internal class SummatorParallelLINQ : SummatorBase
    {
        public SummatorParallelLINQ()
        {
            _name = "Параллельно (LINQ)";
        }

        public override long DoSumm(int[]? array, int degreeOfParallelism)
        {
            if (array is null)
                return 0;

            return array.AsParallel().WithDegreeOfParallelism(degreeOfParallelism).Sum(x => (long)x);
        }

    }
}
