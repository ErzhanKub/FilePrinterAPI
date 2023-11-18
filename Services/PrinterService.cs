using FilePrinterAPI.Interfaces;
using FilePrinterAPI.Models;
using System.Drawing.Printing;

namespace FilePrinterAPI.Services
{
    // Сервис для работы с принтером
    public sealed class PrinterService : IPrinterService
    {
        private readonly ILogger<PrinterService> _logger;

        public PrinterService(ILogger<PrinterService> logger)
        {
            _logger = logger;
        }

        // Асинхронный метод (для того чтобы в будущем получать информацию о принтерах из удаленного источника), который возращает коллекцию моделей Printer
        public Task<IEnumerable<Printer>> GetPrintersInfoAsync()
        {
            try
            {
                // Лист принтеров
                var printers = new List<Printer>();

                // Перебираю все установленые принтеры
                foreach (string name in PrinterSettings.InstalledPrinters)
                {
                    // Создаю экземпляр PrinterSettings с свойством PrinterName = name, чтобы получить доступ к другим свойствам текущего принтера(получаю доступ к настройкам текущего принтера)
                    var printerSettings = new PrinterSettings { PrinterName = name };

                    // Создаю экземпляр Printer
                    var printer = new Printer
                    {
                        // Уставналивая Name из name(PrinterSettings.InstalledPrinters)
                        Name = name,
                        // Устанавливаю статус принтера. Использую тернарную операция для проверки bool IsValid - если true то принтер готов, иначе не недоступен
                        Status = printerSettings.IsValid ? Enums.PrinterStatus.Ready : Enums.PrinterStatus.Unavailable
                    };
                    printers.Add(printer);
                }
                _logger.LogInformation("Successfully get printer info");
                return Task.FromResult(printers.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving printer info");
                throw;
            }
        }
    }
}
