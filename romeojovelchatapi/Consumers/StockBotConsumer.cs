using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using romeojovelchatapi.Helpers;
using romeojovelchatapi.Hubs;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace romeojovelchatbot.Listeners
{

    public class StockBotConsumer : IHostedService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public StockBotConsumer(IOptions<AppSettings> options, IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
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
            _channel.QueueDeclare(queue: "stock.resp",
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
                _hubContext.Clients.All.SendAsync("ReceiveMessage", "stockBot", message);
            };
            _channel.BasicConsume(queue: "stock.resp",
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
