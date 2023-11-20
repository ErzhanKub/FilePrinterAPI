using FilePrinterAPI.Interfaces;
using System.Drawing.Printing;

namespace FilePrinterAPI.Services
{
    public sealed class PrinterProvider : IPrinterProvider
    {
        // Возвращает коллекцию установелных принтеров
        public IEnumerable<string> GetInstalledPrinters()
        {
            return PrinterSettings.InstalledPrinters.Cast<string>();
        }
    }
}