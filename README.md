# FilePrinterAPI
FilePrinterAPI - это веб-сервис, который предоставляет доступ к файлам с определенными расширениями (“.doc”, “.docx”, “.xls”, “.xlsx”, “.pdf”, “.jpg”, “.png”) и позволяет печатать их на подключенных принтерах.

# Особенности
• Получение информации о доступных принтерах и их статусе (готов/занят)

• Получение списка накопителей (дисков) на компьютере

• Получение списка файлов и подкаталогов в указанной директории

• Получение содержимого файла в формате base64

• Печать файла на выбранном принтере (в будущем)

# Установка
Для установки и запуска FilePrinterAPI вам потребуются следующие компоненты:

• .NET 7.0 или выше

# Использование
Для того, чтобы использовать FilePrinterAPI, вам нужно отправлять HTTP-запросы к соответствующим эндпоинтам. Вы можете использовать любой HTTP-клиент, например Postman, или воспользоваться документацией Swagger, если программа запущена в режиме debug.

Вот примеры некоторых запросов и ответов:

**• Получение информации о принтерах**

Запрос: 

GET /api/printers

Ответ: 

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

**• Получение списка накопителей**

Запрос: 

GET /api/file-systems/drives

Ответ: 

JSON [
  "C:\\"
]

**• Получение списка файлов и подкаталогов**

Запрос: 

GET /api/file-systems/files/directoryPath=C:\Users\user\Documents

Ответ:

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

**• Получение содержимого файла**

Запрос:

GET /api/file-systems/files/content/filePath=C:\Users\user\Documents\resume.docx

Ответ:

JSON {
  "Name": "resume.docx",
  "Extension": ".docx",
  "Size": 24576,
  "Path": "C:\\Users\\user\\Documents\\resume.docx",
  "Content": "UEsDBBQABgAIAAAAIQDfpNJsWgEAACAFAAATAAgCW0NvbnRlbnRfVHlwZXNdLnhtbCCiBAIooAAC..."
}

# Планы на будущее
В будущем я планирую добавить следующие функции и улучшения к FilePrinterAPI:

• Поддержка других форматов файлов, таких как “.ppt”, “.txt”, “.csv” и т.д.

• Возможность выбирать параметры печати, такие как ориентация, масштаб, качество и т.д.

• Возможность просматривать содержимое файла в браузере перед печатью

• Возможность загружать файлы с компьютера пользователя или из интернета

• Возможность отправлять файлы на печать по электронной почте или через мобильное приложение

# Лицензия
Nope
