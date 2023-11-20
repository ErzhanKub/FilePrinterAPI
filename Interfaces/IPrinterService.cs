using FilePrinterAPI.Models;

namespace FilePrinterAPI.Interfaces
{
    public interface IPrinterService
    {
        IEnumerable<Printer> GetPrintersInfo();
    }
}