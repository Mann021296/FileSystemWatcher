using System;
using System.IO;

namespace FileSystemWatcher
{
    public class SimpleLogger
    {
        private string logPath;

        public SimpleLogger(string path)
        {
            logPath = path;
            try { Directory.CreateDirectory(Path.GetDirectoryName(path)); } catch { }
        }

        public void LogInfo(string message)
        {
            try
            {
                File.AppendAllText(logPath, $"{DateTime.Now} - INFO: {message}\n");
                System.Diagnostics.EventLog.WriteEntry("FileSystemWatcher", message, System.Diagnostics.EventLogEntryType.Information);
            }
            catch { }
        }

        public void LogError(string message)
        {
            try
            {
                File.AppendAllText(logPath, $"{DateTime.Now} - ERROR: {message}\n");
                System.Diagnostics.EventLog.WriteEntry("FileSystemWatcher", message, System.Diagnostics.EventLogEntryType.Error);
            }
            catch { }
        }
    }
}
