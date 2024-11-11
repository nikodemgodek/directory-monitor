using System;
using System.IO;

namespace DirAnalyzer
{
    public class DirectoryMonitorApp
    {
        private readonly DirectoryMonitor _monitor;

        public DirectoryMonitorApp(string directoryPath)
        {
            _monitor = new DirectoryMonitor(directoryPath);
        }

        public static string GetDirectoryPath()
        {
            string directoryPath;

            while (true)
            {
                Console.WriteLine("Podaj œcie¿kê do folderu, który ma byæ monitorowany\n");
                directoryPath = Console.ReadLine();

                if (Directory.Exists(directoryPath)) break;

                Console.WriteLine("Nieprawid³owa œcie¿ka. Spróbuj ponownie");

            }

            return directoryPath;

        }

        public void StartMonitoring()
        {
            _monitor.StartWatching();
            Console.ReadLine();
        }

        public void StopMonitoring()
        {
            _monitor.StopWatching();
        }
    }
}

