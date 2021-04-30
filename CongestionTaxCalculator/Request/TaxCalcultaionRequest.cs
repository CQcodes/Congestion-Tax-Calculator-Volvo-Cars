using System;

namespace CongestionTaxCalculator.Request
{
    public class TaxCalcultaionRequest
    {
        public string VehicleType { get; set; }
        public DateTime[] Dates {get;set;}
    }
}
