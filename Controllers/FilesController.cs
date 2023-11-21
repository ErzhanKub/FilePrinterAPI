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

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemController"/> class.
        /// </summary>
        /// <param name="fileService">The service that provides access to the file system.</param>
        /// <param name="logger">The logger that records the events.</param>
        public FileSystemController(IFileSystemAccessService fileService, ILogger<FileSystemController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of all drives in the file system.
        /// </summary>
        /// <returns>A list of drive names.</returns>
        /// <example>
        /// GET api/file-systems/drives
        /// Response: ["C:\\", "D:\\", "E:\\"]
        /// </example>
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

        /// <summary>
        /// Retrieves a list of all files.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to search.</param>
        /// <returns>A list of file names, extension, sizes, path, contentBase64.</returns>
        /// <example>
        /// GET api/file-systems/files/c:\users\hp\downloads
        /// Response: [{"name": "Base64ToDOCX", "extension": ".docx", "size": 24595, "path": "c:\\users\\hp\\downloads\\Base64ToDOCX.docx"}, ...]
        /// </example>
        [HttpGet("files/{directoryPath}")]
        [SwaggerOperation(Summary = "Retrieves a list of all files in the specified directory")]
        [SwaggerResponse(StatusCodes.Status200OK, "File list successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while retrieving the file list")]
        public async Task<IActionResult> GetFilesInDirectory(string directoryPath)
        {
            try
            {
                var files = await _fileService.GetAllFilesAsync(directoryPath);
                _logger.LogInformation("GetFilesInDirectory was called with: {directoryPath}", directoryPath);
                return Ok(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFilesInDirectory with: {directoryPath}", directoryPath);
                return BadRequest(new { message = ex.Message, directoryPath });
            }
        }

        /// <summary>
        /// Retrieves the content of the file.
        /// </summary>
        /// <param name="filePath">The path of the file to read.</param>
        /// <returns>The content of the file as a string.</returns>
        /// <example>
        /// GET api/file-systems/files/content/c:\\users\\hp\\downloads\\Base64ToDOCX.docx
        /// Response: [{"name": "Base64ToDOCX", "extension": ".docx", "size": 24595, "path": "c:\\users\\hp\\downloads\\Base64ToDOCX.docx"}, "contentBase64": "UEsDBBQABgAIAAA..."}]
        /// </example>
        [HttpGet("files/content/{filePath}")]
        [SwaggerOperation(Summary = "Retrieves the content of the file at the specified path")]
        [SwaggerResponse(StatusCodes.Status200OK, "File content successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "An error occurred while retrieving the file content")]
        public async Task<IActionResult> GetFileContent(string filePath)
        {
            try
            {
                var file = await _fileService.GetSingleFileAsync(filePath);
                _logger.LogInformation("GetFileContent was called with: {filePath}", filePath);
                return Ok(file);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFileContent with: {filePath}", filePath);
                return BadRequest(new { message = ex.Message, filePath });
            }
        }
    }
}
