using FilePrinterAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilePrinterAPI.Models
{
    public sealed record Printer
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public PrinterStatus Status { get; set; }
    }
}
