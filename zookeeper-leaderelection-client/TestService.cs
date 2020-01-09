using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace zookeeper_leaderelection_client
{
    public class TestService : IHostedService
    {
        private readonly IZooKeeperClient _client;
        private readonly ILogger<TestService> _logger;

        public TestService(IZooKeeperClient client, ILogger<TestService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () => await Run());
            return Task.CompletedTask;
        }

        private async Task Run()
        {
            //We'll do a loop where we check if we're the leader
            while (true)
                //Depending on the service, you can be leader for one, but not for an other
                if (await _client.IsLeader("testservice"))
                {
                    _logger.LogInformation("Look at me... I am the leader now!");
                    //do stuff as the leader
                    return;
                }
        }

        public Task StopAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}
