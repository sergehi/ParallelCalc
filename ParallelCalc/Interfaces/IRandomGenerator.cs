using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelCalc.Interfaces
{
    internal interface IRandomGenerator<T>
    {
        T Next(T minVal, T maxVal);
        T[]? GenerateArray(int Count, T minVal, T maxVal);
    }
}
