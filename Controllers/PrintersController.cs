using Microsoft.AspNetCore.Mvc;

namespace FilePrinterAPI.Controllers
{
    [ApiController]
    [Route("api/printers")]
    public class PrintersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPrinters()
        {

            return Ok(); 
        }
    }
}
