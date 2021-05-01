using System;
using System.Collections.Generic;

namespace CongestionTaxCalculator.Request
{
    public class TaxCalcultaionRequest
    {
        public string VehicleType { get; set; }
        public List<DateTime> Dates {get;set;}
    }
}
