namespace FilePrinterAPI.Interfaces
{
    public interface IPrinterProvider
    {
        IEnumerable<string> GetInstalledPrinters();
    }
}