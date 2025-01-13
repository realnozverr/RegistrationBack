using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Registration.Web.Models;
using System.Text;
using System.Text.Json;

namespace Registration.Web.Services
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IConfiguration _configuration;
        public MessageProducer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMessage(RegisterModel model)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "registerQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            string jsonString = JsonSerializer.Serialize(model);
            byte[] body = Encoding.UTF8.GetBytes(jsonString);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "registerQueue", body: body);
        }
    }
}
