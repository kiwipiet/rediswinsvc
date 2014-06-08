using System;
using System.Configuration;
using System.IO;

namespace RedisWinSvc
{
    internal class ConfigImpl : IConfig
    {
        const string RedisServer = "redis-server.exe";
        const string RedisCLI = "redis-cli.exe";

        public ConfigImpl()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            RedisServerFullPath = ConfigurationManager.AppSettings["RedisServerFullPath"];
            if (string.IsNullOrEmpty(RedisServerFullPath) || !File.Exists(RedisServerFullPath))
            {
                RedisServerFullPath = Path.Combine(basePath, RedisServer);
            }
            RedisCLIFullPath = ConfigurationManager.AppSettings["RedisCLIFullPath"];
            if (string.IsNullOrEmpty(RedisCLIFullPath) || !File.Exists(RedisCLIFullPath))
            {
                RedisCLIFullPath = Path.Combine(basePath, RedisCLI);
            }
            int port = 0;
            if (!int.TryParse(ConfigurationManager.AppSettings["RedisServerPort"], out port))
            {
                port = 6379;
            }
            Port = port;
            ConfigFullPath = ConfigurationManager.AppSettings["ConfigFullPath"];
            WorkingDirectory = ConfigurationManager.AppSettings["WorkingDirectory"] ?? Path.GetDirectoryName(ConfigFullPath);
        }
        public string RedisServerFullPath { get; private set; }
        public string RedisCLIFullPath { get; private set; }
        public int Port { get; private set; }
        public string ConfigFullPath { get; private set; }
        public string WorkingDirectory { get; private set; }
    }
}
