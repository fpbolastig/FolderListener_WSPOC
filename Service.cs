using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;


namespace FolderListener_WSPOC
{
    public partial class ServiceMain : BackgroundService
    {
        private readonly ILogger<ServiceMain> _logger;
        private readonly FileSystemWatcher _watcher;
        private const string SrceFolder = @"C:\Folder1";
        private const string DestFolder = @"C:\Folder2";

        public ServiceMain(ILogger<ServiceMain> logger)
        {
            _logger = logger;

            Directory.CreateDirectory(SrceFolder);
            Directory.CreateDirectory(DestFolder);

            _watcher = new FileSystemWatcher(SrceFolder)
            {
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*", 
                EnableRaisingEvents = true
            };
            _watcher.Created += OnFileCreated;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask; 
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string srcFilePath = e.FullPath;
                string dstFilePath = Path.Combine(DestFolder, e.Name);

                //note: Rather than using File.Move(), I still use the File.Copy(), and check
                //      if both files exists before deleting the source.
                //      This is a common practice, to ensure success copying if a disruption
                //      happens while in the middle of the .Copy process.
                File.Copy(srcFilePath, dstFilePath);

                if (File.Exists(srcFilePath) == true || File.Exists(dstFilePath) == true) 
                {
                    File.Delete(srcFilePath);
                }
                
                //File.Move(srcFilePath, destFilePath);

                _logger.LogInformation($"File moved: {e.Name} from {SrceFolder} to {DestFolder}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error moving file: {e.Name}");
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _watcher.Dispose();
            return base.StopAsync(stoppingToken);
        }
    }
}
