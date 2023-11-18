using FilePrinterAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

// Делаю эндпоинты максимально тонкими без валидации данных.
namespace FilePrinterAPI.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILogger<FilesController> _logger;

        public FilesController(IFileService fileService, ILogger<FilesController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetFiles()
        {

            return Ok();
        }

        [HttpGet("{path}")]
        public IActionResult GetFilesInDirectory(string path)
        {
            try
            {
                var files = _fileService.GetFilesAsync(path);
                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("content/{path}")]
        public async Task<IActionResult> GetFileContent(string path)
        {
            try
            {
                var file = await _fileService.GetFileAsync(path);
                return Ok(file);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
