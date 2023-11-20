using FilePrinterAPI.Models;

namespace FilePrinterAPI.Interfaces
{
    public interface IFileSystemAccessService
    {
        Task<IEnumerable<FileModel>> GetAllFilesAsync(string directoryPath);
        Task<FileModel> GetSingleFileAsync(string filePath);
        IEnumerable<string> GetAllDrives();
    }
}
