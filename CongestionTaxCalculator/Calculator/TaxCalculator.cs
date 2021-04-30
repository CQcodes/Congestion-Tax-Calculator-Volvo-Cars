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
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = taxRule.GetTollFee(date, vehicle);
                int tempFee = taxRule.GetTollFee(intervalStart, vehicle);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }
    }
}
