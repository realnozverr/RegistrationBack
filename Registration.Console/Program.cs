using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Registration.ConsoleConsumer.Services;

namespace Registration.ConsoleConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<IMessageConsume, MessageConsume>();
            IHost host = builder.Build();
        }
    }
}
