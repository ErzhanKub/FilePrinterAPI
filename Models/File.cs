using System.ComponentModel.DataAnnotations;

// Вместо rich models, использую более простой подход с атрибутами - вынес валидацию данных
namespace FilePrinterAPI.Models
{
    public sealed record FileModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Extension { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long Size { get; set; }

        [Required]
        public string Path { get; set; }
        public string ContentBase64 { get; set; }
    }
}