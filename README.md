# FilePrinterAPI
FilePrinterAPI - это веб-сервис, который предоставляет доступ к файлам с определенными расширениями (“.doc”, “.docx”, “.xls”, “.xlsx”, “.pdf”, “.jpg”, “.png”) и позволяет печатать их на подключенных принтерах.

FilePrinterAPI is a web service that provides access to files with specific extensions (“.doc”, “.docx”, “.xls”, “.xlsx”, “.pdf”, “.jpg”, “.png” ) and allows you to print them on connected printers.

# Особенности / Peculiarities

• Получение информации о доступных принтерах и их статусе (готов/занят) / Obtaining information about available printers and their status (ready/busy)

• Получение списка накопителей (дисков) на компьютере / Getting a list of drives (drives) on your computer

• Получение списка файлов и подкаталогов в указанной директории / Getting a list of files and subdirectories in a specified directory

• Получение содержимого файла в формате base64 / Getting file contents in base64 format

• Печать файла на выбранном принтере (в будущем) / Print a file to a selected printer (future)

# Technologies used

1) .Net 7.0
2) ASP.NET WebApi

# Использование / Using

Примеры / Example 

**• Получение информации о принтерах / Getting information about printers**

Запрос / Request: 

GET /api/printers

Ответ / Response: 

JSON [
  {
    "Name": "HP LaserJet 1020",
    "Status": "1"
  },
  {
    "Name": "Canon PIXMA MG3650",
    "Status": "2"
  }
]

**• Получение списка накопителей / Getting a list of drives**

Запрос / Request: 

GET /api/file-systems/drives

Ответ / Response: 

JSON [
  "C:\\"
]

**• Получение списка файлов и подкаталогов / Getting a list of files and subdirectories**

Запрос / Request: 

GET /api/file-systems/files/directoryPath=C:\Users\user\Documents

Ответ / Response:

JSON [
  {
    "Name": "resume.docx",
    "Extension": ".docx",
    "Size": 24576,
    "Path": "C:\\Users\\user\\Documents\\resume.docx"
  },
  {
    "Name": "report.pdf",
    "Extension": ".pdf",
    "Size": 123456,
    "Path": "C:\\Users\\user\\Documents\\report.pdf"
  },
  {
    "Name": "photos",
    "Extension": "directory",
    "Size": 0,
    "Path": "C:\\Users\\user\\Documents\\photos"
  }
]

**• Получение содержимого файла / Getting the contents of a file**

Запрос / Request:

GET /api/file-systems/files/content/filePath=C:\Users\user\Documents\resume.docx

Ответ / Response:

JSON {
  "Name": "resume.docx",
  "Extension": ".docx",
  "Size": 24576,
  "Path": "C:\\Users\\user\\Documents\\resume.docx",
  "Content": "UEsDBBQABgAIAAAAIQDfpNJsWgEAACAFAAATAAgCW0NvbnRlbnRfVHlwZXNdLnhtbCCiBAIooAAC..."
}
