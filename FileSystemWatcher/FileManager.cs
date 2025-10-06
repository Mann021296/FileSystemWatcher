using System;
using System.IO;

namespace FileSystemWatcher
{
    public class FileManager
    {
        private string source;
        private string destination;
        private SimpleLogger logger;

        public FileManager(string src, string dest, SimpleLogger log)
        {
            source = src;
            destination = dest;
            logger = log;
        }

        public void CreateDirectories()
        {
            try
            {
                Directory.CreateDirectory(source);
                Directory.CreateDirectory(destination);
                logger.LogInfo("Directories created");
            }
            catch (Exception ex)
            {
                logger.LogError($"Directory error: {ex.Message}");
            }
        }

        public void MoveFile(string filePath)
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                File.Move(filePath, Path.Combine(destination, Path.GetFileName(filePath)));
                logger.LogInfo($"File moved: {Path.GetFileName(filePath)}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Move error: {ex.Message}");
            }
        }
    }
}
