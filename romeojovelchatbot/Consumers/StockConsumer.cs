using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using romeojovelchatbot.Helpers;
using romeojovelchatbot.Services;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace romeojovelchatbot.Listeners
{

    public class StockConsumer : IHostedService
    {

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IStockCsvParser _stockCsvParser;

        public StockConsumer(IOptions<AppSettings> options, IStockCsvParser stockCsvParser)
        {
            _stockCsvParser = stockCsvParser;
            try
            {
                var factory = new ConnectionFactory()
                {
                    // This is my side of the configuration, you can use it yourself.
                    HostName = options.Value.RabbitMqHost
                    //UserName = options.Value.RabbitUserName,
                    //Password = options.Value.RabbitPassword,
                    //Port = options.Value.RabbitPort,
                };
                this._connection = factory.CreateConnection();
                this._channel = _connection.CreateModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RabbitListener init error,ex:{ex.Message}");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Register();
            return Task.CompletedTask;
        }


        protected string RouteKey;
        protected string QueueName;

        // Registered Consumer Monitor Here
        public void Register()
        {
            _channel.QueueDeclare(queue: "stock.req",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null) ;

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                Console.WriteLine(ea.RoutingKey);
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                _stockCsvParser.SendQuoteAsync(message);
            };
            _channel.BasicConsume(queue: "stock.req",
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void DeRegister()
        {
            this._connection.Close();
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            this._connection.Close();
            return Task.CompletedTask;
        }
    }

}
