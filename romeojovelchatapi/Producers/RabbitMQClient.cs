using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using romeojovelchatapi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace romeojovelchatapi.Producers
{
    public class RabbitMQClient
    {

        private readonly IModel _channel;

        private readonly ILogger _logger;




        public RabbitMQClient(IOptions<AppSettings> options, ILogger<RabbitMQClient> logger)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = options.Value.RabbitMqHost
                    //UserName = options.Value.RabbitUserName,
                    //Password = options.Value.RabbitPassword,
                    //Port = options.Value.RabbitPort,
                };
                var connection = factory.CreateConnection();
                _channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                logger.LogError(-1, ex, "RabbitMQClient init fail");
            }
            _logger = logger;
        }

        public virtual void PushMessage(string message)
        {
            _channel.QueueDeclare(queue: "stock.req", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.BasicPublish(exchange: "", routingKey: "stock.req", basicProperties: null, body: Encoding.UTF8.GetBytes(message));
        }
    }
}

