using System.ComponentModel.DataAnnotations;

namespace FilePrinterAPI.Models
{
    // Небольшая валидация с помошью атрибутов
    // required - для заглушки предупреждения
    public sealed record FileModel
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        public required string Extension { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long Size { get; set; }

        [Required]
        public required string Path { get; set; }
        public string? ContentBase64 { get; set; }
    }
}