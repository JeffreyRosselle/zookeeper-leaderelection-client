using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace zookeeper_leaderelection_client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IZooKeeperClient, ZooKeeperClient>();
            services.AddHostedService<ZooKeeperService>();
            services.AddHostedService<TestService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseWelcomePage();
        }
    }
}
