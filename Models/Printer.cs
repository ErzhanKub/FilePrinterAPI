using FilePrinterAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace FilePrinterAPI.Models
{
    public sealed record Printer
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "Status is required.")]
        public PrinterStatus Status { get; init; }
    }
}
