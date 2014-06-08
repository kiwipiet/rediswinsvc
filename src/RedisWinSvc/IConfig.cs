
namespace RedisWinSvc
{
    public interface IConfig
    {
        string RedisServerFullPath { get; }
        string RedisCLIFullPath { get; }
        int Port { get; }
        string ConfigFullPath { get; }
        string WorkingDirectory { get; }
    }
}
