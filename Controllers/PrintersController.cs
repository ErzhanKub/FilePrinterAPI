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
        [SwaggerOperation(Summary = "Получает информацию о всех доступных принтерах")]
        [SwaggerResponse(StatusCodes.Status200OK, "Информация о принтерах успешно получена")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Произошла ошибка при получении информации о принтерах")]
        public async Task<IActionResult> RetrievePrinters()
        {
            try
            {
                var response = await _printerService.GetPrintersInfoAsync();
                _logger.LogInformation("Successfully retrieved printer info");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving printer info");
                return new StatusCodeResult(500);
            }
        }
    }
}
