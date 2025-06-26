# Системы реального времени

## Проекты

### 1. Disk_Space_Monitoring_Email_Reports
**Мониторинг свободного места на диске с отправкой email отчетов**

- **Приложение:** `DiskMonitorApp`
- **Пространство имен:** `DiskMonitorApp`
- **Функции:**
  - Использование WinAPI функции `GetDiskFreeSpaceEx`
  - SMTP-клиент для отправки отчетов через Gmail
  - Периодический мониторинг с настраиваемым интервалом
  - Отчеты содержат информацию о общем объеме и свободном месте на диске

### 2. User_Inactivity_Monitoring_System
**Система мониторинга неактивности пользователя**

- **Функции:**
  - Глобальный мониторинг движений мыши через WinAPI хуки
  - Таймер неактивности (60 секунд по умолчанию)
  - Обратный отсчет времени до предупреждения
  - Звуковое предупреждение при неактивности
  - Визуальный интерфейс с кнопками управления

### 3. Async_Directory_Copy_With_Progress
**Асинхронное копирование директорий с прогресс-баром**

- **Функции:**
  - Асинхронное копирование файлов и папок
  - Прогресс-бар для отображения процесса копирования
  - Выбор исходной и целевой директорий через диалоги
  - Копирование с сохранением структуры папок
  - Копирование файлов

### 4. Async_Backup_System
**Асинхронная система резервного копирования**

- **Функции:**
  - Полностью асинхронное копирование файлов и папок
  - Использование `FileOptions.Asynchronous` для оптимизации
  - Рекурсивное копирование структуры директорий
  - Выбор папок через FolderBrowserDialog
  - Обработка ошибок и валидация

## Технологии

- **C#** - основной язык программирования
- **WinForms** - пользовательский интерфейс
- **Windows API** - системные функции Windows
- **async/await** - асинхронное программирование
- **SMTP** - отправка email
- **System.Timers** - работа с таймерами

## Требования

- Visual Studio 2019/2022
- .NET Framework 4.7.2 или выше
- Windows 10/11

## Структура проекта

```
├── Disk_Space_Monitoring_Email_Reports/
│   ├── DiskMonitorApp.sln
│   └── DiskMonitorApp/
│       ├── Form1.cs
│       ├── Form1.Designer.cs
│       ├── Program.cs
│       └── DiskMonitorApp.csproj
├── User_Inactivity_Monitoring_System/
│   ├── InactivityMonitorApp.sln
│   └── InactivityMonitorApp/
│       ├── Form1.cs
│       ├── Form1.Designer.cs
│       ├── Program.cs
│       └── InactivityMonitorApp.csproj
├── Async_Directory_Copy_With_Progress/
│   ├── DirectoryCopyApp.sln
│   └── DirectoryCopyApp/
│       ├── Form1.cs
│       ├── Form1.Designer.cs
│       ├── Program.cs
│       └── DirectoryCopyApp.csproj
├── Async_Backup_System/
│   ├── BackupApp.sln
│   └── BackupApp/
│       ├── Form1.cs
│       ├── Form1.Designer.cs
│       ├── Program.cs
│       └── BackupApp.csproj
```
