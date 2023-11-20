using System.ComponentModel.DataAnnotations;

namespace FilePrinterAPI.Models
{
    public sealed record FileModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public required string Name { get; init; }

        [Required(ErrorMessage = "Extension is required.")]
        public required string Extension { get; init; }

        [Required(ErrorMessage = "Size is required.")]
        [Range(0, long.MaxValue, ErrorMessage = "Size must be a positive number.")]
        public long Size { get; init; }

        [Required(ErrorMessage = "Path is required.")]
        public required string Path { get; init; }

        public string? ContentBase64 { get; init; }
    }
}
