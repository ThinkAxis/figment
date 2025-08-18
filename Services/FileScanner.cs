using System;
using System.IO;

namespace figment
{
    public class FileScanner
    {
        public void TraverseDirectory(string rootPath)
        {
            // Validate directory path here
            if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
            {
                Console.WriteLine("Invalid or non-existent directory path.");
                return;
            }

            // Ensure Data directory exists
            string dataDir = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            Directory.CreateDirectory(dataDir);

            // Create log file with timestamp
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string logFile = Path.Combine(dataDir, $"FileList_{timestamp}.txt");

            using var writer = new StreamWriter(logFile);

            try
            {
                WriteFilesRecursively(rootPath, writer);
            }
            catch (Exception ex)
            {
                writer.WriteLine($"[FATAL ERROR] {rootPath}: {ex.Message}");
            }
        }

        private void WriteFilesRecursively(string path, StreamWriter writer)
        {
            try
            {
                foreach (var file in Directory.EnumerateFiles(path))
                {
                    writer.WriteLine(file);
                }

                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    WriteFilesRecursively(dir, writer);
                }
            }
            catch (UnauthorizedAccessException)
            {
                writer.WriteLine($"[ACCESS DENIED] {path}");
            }
            catch (Exception ex)
            {
                writer.WriteLine($"[ERROR] {path}: {ex.Message}");
            }
        }
    }
}
