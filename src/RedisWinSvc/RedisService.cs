using System;
using System.Diagnostics;

namespace RedisWinSvc
{
    internal class RedisService : IDisposable
    {
        private readonly IConfig _config;
        private Process _redisProcess;
        public RedisService(IConfig config)
        {
            _config = config;
        }
        public RedisService()
            : this(new ConfigImpl())
        {

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_redisProcess != null)
                {
                    _redisProcess.Dispose();
                    _redisProcess = null;
                }
            }
        }
        public void Start()
        {
            var pi = new ProcessStartInfo(_config.RedisServerFullPath) { Arguments = _config.ConfigFullPath, WorkingDirectory = _config.WorkingDirectory };
            if (!Environment.UserInteractive)
            {
                pi.CreateNoWindow = true;
            }

            if (_redisProcess != null)
            {
                _redisProcess.Exited -= OnRedisProcess_Exited;
                _redisProcess.Dispose();
            }

            _redisProcess = new Process { StartInfo = pi, EnableRaisingEvents = true };
            _redisProcess.Exited += OnRedisProcess_Exited;

            StartRedisProcess();
        }

        void OnRedisProcess_Exited(object sender, EventArgs e)
        {
            StartRedisProcess();
        }

        private void StartRedisProcess()
        {
            if (!_redisProcess.Start())
                throw new RedisServiceException("Failed to start Redis process");
        }
        public void Stop()
        {
            var pi = new ProcessStartInfo(_config.RedisCLIFullPath) { Arguments = (_config.Port == 0 ? "" : String.Format("-p {0} ", _config.Port)) + "shutdown" };

            if (!(new Process { StartInfo = pi }).Start())
                throw new RedisServiceException("Failed to stop Redis process");
        }
    }
}
