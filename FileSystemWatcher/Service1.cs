using System;
using System.IO;
using System.ServiceProcess;

namespace FileSystemWatcher
{
    public partial class Service1 : ServiceBase
    {
        private System.IO.FileSystemWatcher watcher;
        private FileManager fileManager;
        private SimpleLogger logger;

        public Service1()
        {
            InitializeComponent();
            try
            {
                logger = new SimpleLogger(@"C:\Users\EmmanuelJoshuaPaglin\Desktop\FileSystemWatcher\Logs\FileSystemWatcher.log");
                fileManager = new FileManager(@"C:\Users\EmmanuelJoshuaPaglin\Desktop\FileSystemWatcher\Folder1", @"C:\Users\EmmanuelJoshuaPaglin\Desktop\FileSystemWatcher\Folder2", logger);
                fileManager.CreateDirectories();
                SetupWatcher();
                logger.LogInfo("Service ready");
            }
            catch (Exception ex)
            {
                logger?.LogError($"Init error: {ex.Message}");
            }
        }

        private void SetupWatcher()
        {
            try
            {
                watcher = new System.IO.FileSystemWatcher(@"C:\Users\EmmanuelJoshuaPaglin\Desktop\FileSystemWatcher\Folder1");
                watcher.Created += (s, e) => { try { fileManager.MoveFile(e.FullPath); } catch (Exception ex) { logger.LogError($"Move error: {ex.Message}"); } };
                watcher.Error += (s, e) => { try { logger.LogError($"Watcher error: {e.GetException().Message}"); } catch { } };
                watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                logger.LogError($"Watcher setup error: {ex.Message}");
            }
        }

        protected override void OnStart(string[] args)
        {
            try { logger.LogInfo("Service started"); } catch { }
        }

        protected override void OnStop()
        {
            try
            {
                watcher?.Dispose();
                logger.LogInfo("Service stopped");
            }
            catch { }
        }
    }
}
