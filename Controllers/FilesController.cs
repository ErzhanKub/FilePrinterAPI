using FilePrinterAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FilePrinterAPI.Controllers
{
    [ApiController]
    [Route("api/file-systems")]
    public class FileSystemController : ControllerBase
    {
        private readonly IFileSystemAccessService _fileService;
        private readonly ILogger<FileSystemController> _logger;

        public FileSystemController(IFileSystemAccessService fileService, ILogger<FileSystemController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("drives")]
        [SwaggerOperation(Summary = "Получает список всех накопителей")]
        [SwaggerResponse(StatusCodes.Status200OK, "Список накопителей успешно получено")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Произошла ошибка при получении списка накопителей")]
        public IActionResult GetDrives()
        {
            try
            {
                var drives = _fileService.GetAllDrives();
                _logger.LogInformation("GetDrives was called");
                return Ok(drives);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetDrives");
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("files/{path}")]
        [SwaggerOperation(Summary = "Получает список всех файлов в указанной директории")]
        [SwaggerResponse(StatusCodes.Status200OK, "Список файлов успешно получено")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Произошла ошибка при получении списка файлов")]
        public IActionResult GetFilesInDirectory(string path)
        {
            try
            {
                var files = _fileService.GetAllFilesAsync(path);
                _logger.LogInformation("GetFilesInDirectory was called with: {path}", path);
                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFilesInDirectory with: {path}", path);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("files/content/{path}")]
        [SwaggerOperation(Summary = "Получает содержимое файла по указанному пути")]
        [SwaggerResponse(StatusCodes.Status200OK, "Содержимое файла успешно получено")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Произошла ошибка при получении содержимого файла")]
        public async Task<IActionResult> GetFileContent(string path)
        {
            try
            {
                var file = await _fileService.GetSingleFileAsync(path);
                _logger.LogInformation("GetFileContent was called with: {path}", path);
                return Ok(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFileContent with: {path}", path);
                return BadRequest(ex.Message);
            }
        }
    }
}
