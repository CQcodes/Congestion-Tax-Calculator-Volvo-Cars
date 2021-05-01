using System;
using System.Linq;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Calculator
{
    public interface ITaxCalculator
    {
        int GetTax(string vehicle, List<DateTime> dates);
    }

    public class TaxCalculator: ITaxCalculator
    {
        private readonly ITaxRule taxRule;

        public TaxCalculator(ITaxRule _taxRule)
        {
            taxRule = _taxRule;
        }

        public int GetTax(string vehicle, List<DateTime> dates)
        {
            var datesDictionary = dates.GroupBy(g=>g.Date).ToDictionary(k=>k.Key,v=> v.ToList());
            int grandTotal = 0;

            foreach(var day in datesDictionary)
            {
                List<DateTime> tollCrossingTimestamps = day.Value;
                tollCrossingTimestamps.Sort();

                DateTime initialTollCrossing = tollCrossingTimestamps[0];
                int initialTollFee = taxRule.GetTollFee(initialTollCrossing, vehicle);
                int totalFeeOfTheDay = initialTollFee;

                foreach (DateTime tollCrossingTimestamp in tollCrossingTimestamps)
                {
                    int currentTollFee = taxRule.GetTollFee(tollCrossingTimestamp, vehicle);

                    double minutesSinceLastTollCrossing = (tollCrossingTimestamp - initialTollCrossing).TotalMinutes;

                    if (minutesSinceLastTollCrossing <= 60)
                    {
                        if (currentTollFee > initialTollFee)
                        {
                            totalFeeOfTheDay = totalFeeOfTheDay - initialTollFee + currentTollFee;
                            initialTollFee = currentTollFee;
                        }
                    }
                    else
                    {
                        totalFeeOfTheDay = totalFeeOfTheDay + currentTollFee;
                        initialTollCrossing = tollCrossingTimestamp;
                        initialTollFee = currentTollFee;
                    }
                }

                if (totalFeeOfTheDay > 60) // per day per vehicle max fee is 60 SEK
                    totalFeeOfTheDay = 60;

                grandTotal = grandTotal + totalFeeOfTheDay;
            }

            return grandTotal;
        }
    }
}
