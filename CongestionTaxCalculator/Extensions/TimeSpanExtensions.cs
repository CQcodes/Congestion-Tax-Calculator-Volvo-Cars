using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Extensions
{
    public static class TimeSpanExtensions
    {
        public static bool IsBetweenInterval(this TimeSpan t, TimeSpan from, TimeSpan to)
        {
            return t >= from && t <= to;
        }
    }
}
