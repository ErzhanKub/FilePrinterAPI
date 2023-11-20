using FilePrinterAPI.Interfaces;
using FilePrinterAPI.Models;
using System.Drawing.Printing;
using FilePrinterAPI.Enums;

namespace FilePrinterAPI.Services
{
    // сервис для работы с принтером
    public sealed class PrinterService : IPrinterService
    {
        // Закрытые поля для логгера и провайдера принтеров
        private readonly ILogger<PrinterService> _logger;
        private readonly IPrinterProvider _printerProvider;

        public PrinterService(ILogger<PrinterService> logger, IPrinterProvider printerProvider)
        {
            _logger = logger;
            _printerProvider = printerProvider;
        }

        // Метод для получения информации о принтерах
        public IEnumerable<Printer> GetPrintersInfo()
        {
            var printers = new List<Printer>();

            // Перебор всех установленных принтеров
            foreach (string name in _printerProvider.GetInstalledPrinters())
            {
                // Создаю экземпляр PrinterSettings с свойством PrinterName = name,
                // чтобы получить доступ к другим свойствам текущего принтера(получаю доступ к настройкам текущего принтера)
                var printerSettings = new PrinterSettings { PrinterName = name };

                // Создание экземпляра принтера с заданными свойствами
                var printer = new Printer
                {
                    Name = name,
                    // Устанавливаю статус принтера. Использую тернарную операция для проверки bool IsValid - если true то принтер готов, иначе не недоступен
                    Status = printerSettings.IsValid ? PrinterStatus.Ready : PrinterStatus.Unavailable
                };
                // Добавление принтера в список
                printers.Add(printer);
            }

            // Логирование успешного получения информации о принтерах
            _logger.LogInformation("Successfully get printer info");
            // Возвращение списка принтеров
            return printers;
        }
    }
}
