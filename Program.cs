using System;
using System.IO;
using Serilog;

namespace DirAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string directoryPath = DirectoryMonitorApp.GetDirectoryPath();
                var app = new DirectoryMonitorApp(directoryPath);

                app.StartMonitoring();
                app.StopMonitoring();
            } catch (Exception ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }

    }
}
