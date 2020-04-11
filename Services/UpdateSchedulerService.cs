using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AspAutoSelfUpdater.Services
{
    public class UpdateSchedulerService : IHostedService, IDisposable
    {

        private Timer _timer;
        private object _lock = "";
        public UpdateSchedulerService()
        {
        }

        void StartService(object parameters)
        {
            try
            {
                lock (_lock)
                {
                    if (!UpdaterService.CheckNewVersion())
                        return;

                    if (!UpdaterService.DownloadNewVersion())
                        return;
                    
                    while (!UpdaterService.StartUpdate())
                        System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Log("Error", ex.ToString());
            }

        }

        private void Log(params string[] message)
        {
            try
            {
                using (var file = new StreamWriter("ContractStatusUpdateService.txt", append: true))
                {
                    file.WriteLine($"{DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss")} - {string.Join(", ", message)}");
                }
            }
            catch { }
        }

        public Task StartAsync(System.Threading.CancellationToken cancellationToken)
        {
            if (SettingService.Setting.CheckIntervalEnable) return Task.Delay(10);            
            _timer = new Timer(StartService, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(SettingService.Setting.CheckIntervalSeconds));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion



    }
}
