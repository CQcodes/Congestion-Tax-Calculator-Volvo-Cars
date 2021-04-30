using CongestionTaxCalculator.Extensions;
using System;

namespace CongestionTaxCalculator.Calculator
{
    public interface ITaxRule
    {
        int GetTollFee(DateTime date, string vehicle);
        bool IsTollFreeDate(DateTime date);
        bool IsTollFreeVehicle(string vehicle);
    }

    public class TaxRule : ITaxRule
    {
        public int GetTollFee(DateTime date, string vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) 
                return 0;

            if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(06, 00, 00), new TimeSpan(06, 29, 00)))      //06:00-06:29
                return 8;                                                                                  
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(06, 30, 00), new TimeSpan(06, 59, 00))) //06:30-06:59
                return 13;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(07, 00, 00), new TimeSpan(07, 59, 00))) //07:00-07:59
                return 18;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(08, 00, 00), new TimeSpan(08, 29, 00))) //08:00-08:29
                return 13;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(08, 30, 00), new TimeSpan(14, 59, 00))) //08:30-14:59
                return 8;                                                                                  
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(15, 00, 00), new TimeSpan(15,29, 00)))  //15:00-15:29
                return 13;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(15, 30, 00), new TimeSpan(16, 59, 00))) //15:30-16:59
                return 18;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(17, 00, 00), new TimeSpan(17, 59, 00))) //17:00-17:59
                return 13;                                                                                 
            else if (date.TimeOfDay.IsBetweenInterval(new TimeSpan(18, 00, 00), new TimeSpan(18, 29, 00))) //18:00-18:29
                return 8;                                                                                  
            else                                                                                           
                return 0;                                                                                  //18:30-05:59
        }

        public bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                // This can be converted to a list of DateTime to make it more readable.
                // and the check if 2013TollFreeDates.Contains(inputDate).
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTollFreeVehicle(string vehicle)
        {
            if (vehicle == null) 
                return false;

            return Enum.IsDefined(typeof(TollFreeVehicles), vehicle);
        }

        private enum TollFreeVehicles
        {
            Motorcycle = 0,
            Bus = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5,
        }
    }
}
