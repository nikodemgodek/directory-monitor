using System;
using System.IO;
using Serilog;

namespace DirAnalyzer
{
    public class DirectoryMonitor
    {
        private readonly FileSystemWatcher _watcher;
        string logPath = @"<<LOG_PATH>>"; // example: @"C:\Users\johndoe\logs.log"

        public DirectoryMonitor(string directoryPath)
        {

            if (!Directory.Exists(directoryPath))
            {
                throw new ArgumentException("Scie¿ka jest nieprawid³owa");
            }

            ConfigureLogger();

            _watcher = new FileSystemWatcher(directoryPath);

            ConfigureMonitor();
        }


        private void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.File(logPath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{ Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }

        private void ConfigureMonitor()
        {
            _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            _watcher.Created += OnCreated;
            _watcher.Renamed += OnRenamed;
            _watcher.Deleted += OnDeleted;
            _watcher.Changed += OnChanged;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            Log.Information($"Utworzono plik: {e.FullPath}");
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Log.Information($"Usuniêto plik: {e.FullPath}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Log.Information($"Zmodyfikowano plik: {e.FullPath}");
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            Log.Information($"Zmmieniono nazwê pliku: {e.OldFullPath} na {e.FullPath}");
        }

        public void StartWatching()
        {
            _watcher.EnableRaisingEvents = true;
            Console.WriteLine("Monitorowanie folderu rozpoczête. Przycisk [Enter], aby zatrzymaæ");
        }

        public void StopWatching()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
            Log.CloseAndFlush();
            Console.WriteLine("Monitorowanie folderu zatrzymane.");
        }
    }
}