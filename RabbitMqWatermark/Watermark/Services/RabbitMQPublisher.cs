using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Watermark.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMQClientService _rabbitmqClientService; //Buraya interface yapılarak iletişim sağlanabilir.

        public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitmqClientService = rabbitMQClientService;
        }

        public void Publish(productImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitmqClientService.Connect();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            //mesajın rabbitMQde fiziksel olarak kaydedilsin.
            var properties = channel.CreateBasicProperties();

            properties.Persistent = true;

            //publish edilmesi
            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName,
               routingKey: RabbitMQClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);

        }
    }
}
