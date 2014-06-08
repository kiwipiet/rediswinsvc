using Topshelf;

namespace RedisWinSvc
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.StartAutomatically();
                x.Service<RedisService>(s =>
                {
                    s.ConstructUsing(name => new RedisService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Redis Windows Service");
                x.SetDisplayName("Redis");
                x.SetServiceName("Redis");
            });
        }
    }
}