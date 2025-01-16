using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Registration.ConsoleConsumer.Services;

namespace Registration.ConsoleConsumer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<IMessageConsume, MessageConsume>();
            IHost host = builder.Build();
            var messageConsume = host.Services.GetRequiredService<IMessageConsume>();
            await messageConsume.Consume();
        }
    }
}
