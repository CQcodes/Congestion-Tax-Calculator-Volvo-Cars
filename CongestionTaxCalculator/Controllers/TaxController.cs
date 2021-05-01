using CongestionTaxCalculator.Calculator;
using CongestionTaxCalculator.Request;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTaxCalculator.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxCalculator taxCalculator;
        public TaxController(ITaxCalculator _taxCalculator)
        {
            taxCalculator = _taxCalculator;
        }

        [HttpPost]
        [Route("calculate")]
        public IActionResult GetTax(TaxCalcultaionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.VehicleType))
                return BadRequest("Vehicle is a required field.");

            if (request.Dates == null)
                return BadRequest("Dates is a required field.");

            if (request.Dates.Count == 0)
                return BadRequest("Dates can not be empty.");

            return Ok(taxCalculator.GetTax(request.VehicleType,request.Dates));
        }
    }
}
