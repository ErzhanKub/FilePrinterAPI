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
        [SwaggerOperation(Summary = "Retrieves a list of all drives")]
        [SwaggerResponse(StatusCodes.Status200OK, "Drive list successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the list of drives")]
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("files/{path}")]
        [SwaggerOperation(Summary = "Retrieves a list of all files in the specified directory")]
        [SwaggerResponse(StatusCodes.Status200OK, "File list successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while retrieving the file list")]
        public async Task<IActionResult> GetFilesInDirectory(string path)
        {
            try
            {
                var files = await _fileService.GetAllFilesAsync(path);
                _logger.LogInformation("GetFilesInDirectory was called with: {path}", path);
                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFilesInDirectory with: {path}", path);
                return BadRequest(new { message = ex.Message, path });
            }
        }

        [HttpGet("files/content/{path}")]
        [SwaggerOperation(Summary = "Retrieves the content of the file at the specified path")]
        [SwaggerResponse(StatusCodes.Status200OK, "File content successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while retrieving the file content")]
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
                return BadRequest(new { message = ex.Message, path });
            }
        }
    }
}
