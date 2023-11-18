using FilePrinterAPI.Models;

namespace FilePrinterAPI.Interfaces
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetFilesAsync(string path);
        Task<FileModel> GetFileAsync(string path);
    }
}
