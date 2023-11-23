using FilePrinterAPI.Interfaces;
using FilePrinterAPI.Models;
using FluentValidation;
using System.IO;

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
            ValidatePath(path);

            // Получение информации о файле и проврка, использую класс FileInfo
            var file = new FileInfo(path);
            EnsureFileExists(file);

            try
            {

                // Считывание содержимое бинарного файла в массив байтов
                var fileDate = await File.ReadAllBytesAsync(path);

                // Конвертация массива байтов в формат base64
                var base64File = Convert.ToBase64String(fileDate);

                // Создание и заполнение модели
                var fileModel = CreateFileModel(file, base64File);

                _logger.LogInformation("Successfully get file at {path}", path);
                return fileModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving the file at {path}", path);
                throw;
            }
        }

        // метод для получения название накопителей в виде строки(для избежание цикличной сериализации/десериализации)
        public IEnumerable<string> GetAllDrives() => DriveInfo.GetDrives().Select(d => d.Name);

        // Асинхронный метод для получения всех файлов с расширением соотвествующие _allowedExtensions, без контента
        public async Task<IEnumerable<FileModel>> GetAllowedFilesAndDirectoriesAsync(string path)
        {
            // Проверка на то что путь не пустой и коррекнтный
            ValidatePath(path);

            // Проверка на существование директория
            var directory = new DirectoryInfo(path);
            EnsureDirectoryExists(directory);

            try
            {
                // Фильтрация по разрешенным расширениям и создание модели
                var filesAndDirectories = FilterAndMapEntities(directory);

                _logger.LogInformation("Successfully retrieved files from {path}", path);
                return filesAndDirectories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while receiving files from {path}", path);
                throw;
            }
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path) || !Path.IsPathFullyQualified(path))
            {
                throw new ArgumentException("Path is not valid");
            }
        }

        private void EnsureDirectoryExists(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                _logger.LogError("Directory at {path} does not exist", directory.FullName);
                throw new DirectoryNotFoundException($"Directory at {directory.FullName} does not exist");
            }
        }

        private void EnsureFileExists(FileInfo file)
        {
            if (!file.Exists)
            {
                _logger.LogError("File at {path} does not exist", file.FullName);
                throw new FileNotFoundException($"File at {file.FullName} does not exist");
            }
        }

        private IEnumerable<FileModel> FilterAndMapEntities(DirectoryInfo directory)
        {
            // entity is FileInfo file - проверка, что текущий элемент entity является объектом типа FileInfo, если да то присваиваем этот обьект к переменой file и далтше проверяме расиширение текущего файла. 
            // entity is DirectoryInfo - проверка, что текущий элемент entity является объектом типа DirectoryInfo.
            return directory.GetFileSystemInfos().Where(entity => entity is FileInfo file && _allowedExtensions.Contains(file.Extension.ToLowerInvariant()) || entity is DirectoryInfo)
                .Select(entity => new FileModel
                {
                    Name = entity.Name,
                    // В расширение обьекта FileModel мы присваиваем расширение файла, если это файл, иначе присваиваем "directory" - говоря что это папка.
                    // P.s. можно было вместо "directory" присвоить null.
                    Extension = entity is FileInfo file ? file.Extension.ToLowerInvariant() : "directory",
                    // Здесь такая же логика как и с расширением.
                    Size = entity is FileInfo file1 ? file1.Length : 0,
                    Path = entity.FullName,
                });
        }

        private FileModel CreateFileModel(FileInfo file, string base64File)
        {
            return new FileModel
            {
                // Имя файла
                Name = file.Name,
                // Расширение файла в нижнем регистре
                Extension = file.Extension.ToLowerInvariant(),
                // Размер (длина) файла
                Size = file.Length,
                // Полный путь
                Path = file.FullName,
                // Закодированный контент
                ContentBase64 = base64File,
            };
        }
    }
}
