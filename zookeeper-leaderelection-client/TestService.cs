using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace zookeeper_leaderelection_client
{
    public class TestService : BackgroundService
    {
        private readonly IZooKeeperClient _client;
        private readonly ILogger<TestService> _logger;

        public TestService(IZooKeeperClient client, ILogger<TestService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //We'll do a loop where we check if we're the leader
            while (!stoppingToken.IsCancellationRequested)
                //Depending on the service, you can be leader for one, but not for an other
                if (await _client.IsLeader("testservice"))
                {
                    _logger.LogInformation("Look at me... I am the leader now!");
                    //do stuff as the leader
                    return;
                }
        }
    }
}
