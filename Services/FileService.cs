using FilePrinterAPI.Interfaces;
using FilePrinterAPI.Models;
using FluentValidation;

namespace FilePrinterAPI.Services
{
    // Сервис для работы с файлами
    public sealed class FileService : IFileSystemAccessService
    {
        // Логирование
        private readonly ILogger<FileService> _logger;
        // Приватный статистический массив строк - содержит расширение 
        private static readonly string[] _allowedExtensions = { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".jpg", ".png" };

        public FileService(ILogger<FileService> logger)
        {
            _logger = logger;
        }

        // Асинхронный метод для получение данных файла и контент в формате base64
        public async Task<FileModel> GetSingleFileAsync(string path)
        {
            try
            {
                // Получение информации о файле, использую класс FileInfo
                var fileInfo = new FileInfo(path);

                // Считывание содержимое бинарного файла в массив байтов
                var fileDate = await File.ReadAllBytesAsync(path);

                // Конвертация массива байтов в формат base64
                var base64File = Convert.ToBase64String(fileDate);

                // Создание и заполнение модели
                var fileModel = new FileModel
                {
                    // Имя файла
                    Name = fileInfo.Name,
                    // Расширение файла в нижнем регистре
                    Extension = fileInfo.Extension.ToLower(),
                    // Размер (длина) файла
                    Size = fileInfo.Length,
                    // Полный путь
                    Path = fileInfo.FullName,
                    // Закодированный контент
                    ContentBase64 = base64File,
                };

                _logger.LogInformation("Successfully get file at {path}", path);
                return fileModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving the file at {path}", path);
                throw;
            }
        }

        // Асинхронный метод для получения всех файлов с расширением соотвествующие _allowedExtensions, без контента
        public async Task<IEnumerable<FileModel>> GetAllFilesAsync(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories) // Получение всех файлов, "*.*" - это маска поиска(соответствует всем файлам). AllDirectories - поиск включать все подкаталоги
                    .Where(file => _allowedExtensions.Contains(Path.GetExtension(file).ToLower())) // Фильрация по регистру, оставляет только те, у которых регситр соотвествует Contains содержимого в списке _allowedExtensions.
                    .Select(file => new FileModel // Преобразует кайждый файл в FileModel
                    {
                        // Имя файла, получаем из метода GetFileName класса Path
                        Name = Path.GetFileNameWithoutExtension(file),
                        // Расширение в ниждем регистре
                        Extension = Path.GetExtension(file).ToLower(),
                        // Размер(длина)
                        Size = new FileInfo(file).Length,
                        // Путь
                        Path = file
                    });
                _logger.LogInformation("Successfully retrieved files from {path}", path);
                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving files from {path}", path);
                throw;
            }
        }
        // Асихронный метод для получения название накопителей в виде строки(для избежание цикличной сериализации/десериализации)
        public async Task<IEnumerable<string>> GetAllDrives() => DriveInfo.GetDrives().Select(d => d.Name);
    }
}