using RabbitMQ.Client;

namespace Watermark.Services
{
    public class RabbitMQClientService:IDisposable
    {
        //RabbitMQya bağlanmak için bir connection factory alıyoruz.
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;   //kanalın alınması
        public static string ExchangeName = "ImageDirectExchange";  //Direct exchange kullanılacaktır.
        public static string RoutingWatermark = "watermark-route-image";
        public static string QueueName = "queue-watermark-image";

        private readonly ILogger<RabbitMQClientService> _logger;

        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        //bağlantı kurma işlemi gerçekleştirilecek.
        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();

            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);

            _channel.QueueDeclare(QueueName, true, false, false);

            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingWatermark);

            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu ...");

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();

            _logger.LogInformation("RabbitMq ile bağlantı koptu ...");

        }
    }
}
