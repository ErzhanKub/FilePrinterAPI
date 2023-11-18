using FilePrinterAPI.Models;

namespace FilePrinterAPI.Interfaces
{
    public interface IPrinterService
    {
        Task<IEnumerable<Printer>> GetPrintersInfoAsync();
    }
}
