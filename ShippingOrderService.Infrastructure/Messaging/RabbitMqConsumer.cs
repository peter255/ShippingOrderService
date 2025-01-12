using System.Text;
using Newtonsoft.Json;
using PurchaseOrderService.Domain.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ShippingOrderService.Domain.Entities;
using ShippingOrderService.Domain.Interfaces;

namespace ShippingOrderService.Infrastructure.Messaging
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {

        private readonly IShippingOrderRepository _repository;

        public RabbitMqConsumer(IShippingOrderRepository repository)
        {
            _repository = repository;
        }


        public async Task ReciveMessageAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            var queue = await channel.QueueDeclareAsync("shipingOrders");


            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var shippingOrder = JsonConvert.DeserializeObject<ShippingOrder>(message);
                if (shippingOrder != null)
                {
                    await _repository.AddAsync(shippingOrder);
                    await _repository.SaveChangesAsync();
                }
            };

            await channel.BasicConsumeAsync(queue: "shipingOrders", autoAck: true, consumer: consumer);
        }
    }
}
