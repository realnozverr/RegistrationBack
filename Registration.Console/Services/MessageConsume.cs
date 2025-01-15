using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Configuration;
using Registration.ConsoleConsumer.Services;
using System.Text.Json;
using Registration.ConsoleConsumer.Models;

namespace Registration.ConsoleConsumer.Services
{
    public class MessageConsume : IMessageConsume
    {
        private readonly IConfiguration _configuration;
        public MessageConsume(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Consume()
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

            Console.WriteLine("жду сообщения");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                RegModel regModel = JsonSerializer.Deserialize<RegModel>(message);
                Console.WriteLine($"{regModel.DateTime.ToString("yyyy.MM.dd HH:mm")} {regModel.Email} код: {regModel.Code}");
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync("registerQueue", autoAck: true, consumer: consumer);

            Console.WriteLine("тыкните чтобы выйти");
            Console.ReadLine();
        }
    }
}
