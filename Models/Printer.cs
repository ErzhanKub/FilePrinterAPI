using FilePrinterAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilePrinterAPI.Models
{
    // Небольшая валидация с помошью атрибутов
    // required - для заглушки предупреждения
    public sealed record Printer
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public PrinterStatus Status { get; set; }
    }
}
