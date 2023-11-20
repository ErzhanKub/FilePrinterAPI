using FilePrinterAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FilePrinterAPI.Controllers
{
    [ApiController]
    [Route("api/printers")]
    public class PrinterInfoController : ControllerBase
    {
        private readonly ILogger<PrinterInfoController> _logger;
        private readonly IPrinterService _printerService;

        public PrinterInfoController(ILogger<PrinterInfoController> logger, IPrinterService printerService)
        {
            _logger = logger;
            _printerService = printerService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves information about all available printers")]
        [SwaggerResponse(StatusCodes.Status200OK, "Printer information successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while retrieving printer information")]
        public IActionResult RetrievePrinters()
        {
            try
            {
                var response = _printerService.GetPrintersInfo();
                _logger.LogInformation("Successfully retrieved printer info");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving printer info");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
