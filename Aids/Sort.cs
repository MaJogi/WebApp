using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Aids
{
    public static class Sort
    {
        public static void Upwards<T>(ref T min, ref T max) where T : IComparable
        {
            if (min.CompareTo(max) <= 0) return;
            var d = min;
            min = max;
            max = d;
        }
    }
}
