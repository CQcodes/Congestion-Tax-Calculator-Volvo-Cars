using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Request
{
    public class TaxCalcultaionRequest
    {
        public string VehicleType { get; set; }
        public DateTime[] Dates {get;set;}
    }
}
