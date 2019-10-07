using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace zookeeper_leaderelection_client
{
    public class ZooKeeperService : IHostedService
    {
        private readonly IZooKeeperClient _zookeeperClient;

        public ZooKeeperService(IZooKeeperClient zookeeperClient)
        {
            _zookeeperClient = zookeeperClient ?? throw new ArgumentNullException(nameof(zookeeperClient));
        }


        public async Task StartAsync(CancellationToken cancellationToken) =>
            await _zookeeperClient.Connect();

        public async Task StopAsync(CancellationToken cancellationToken) =>
            await _zookeeperClient.Disconnect();
    }
}