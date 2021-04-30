using System;

namespace CongestionTaxCalculator.Calculator
{
    public interface ITaxCalculator
    {
        int GetTax(string vehicle, DateTime[] dates);
    }

    public class TaxCalculator: ITaxCalculator
    {
        private readonly ITaxRule taxRule;

        public TaxCalculator(ITaxRule _taxRule)
        {
            taxRule = _taxRule;
        }

        public int GetTax(string vehicle, DateTime[] dates)
        {
            Array.Sort(dates);

            DateTime intervalStart = dates[0];
            int intervalGreatestFee = taxRule.GetTollFee(intervalStart, vehicle);
            int totalFee = intervalGreatestFee;

            foreach (DateTime date in dates)
            {
                int nextFee = taxRule.GetTollFee(date, vehicle);

                double minutes = (date - intervalStart).TotalMinutes;

                if (minutes <= 60)
                {
                    if (nextFee > intervalGreatestFee)
                    {
                        totalFee = totalFee - intervalGreatestFee + nextFee;
                        intervalGreatestFee = nextFee;
                    }
                }
                else
                {
                    totalFee = totalFee + nextFee;

                    intervalStart = date;
                    intervalGreatestFee = nextFee;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }
    }
}
