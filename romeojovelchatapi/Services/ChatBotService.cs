using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace romeojovelchatapi.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly RabbitMQClient _rabbitListener;
        //private readonly RabbitMQPersistentConnection _rabbitMQConnection;

        public ChatBotService(IHttpClientFactory httpClientFactory, RabbitMQClient rabbitListener)
        {
            _clientFactory = httpClientFactory;
            _rabbitListener = rabbitListener;
        }
        public async Task<string> RequestMessageAsync(MessageResponse content)
        {
            await Task.Yield();
            _rabbitListener.PushMessage(content.Code);
            return "";
        }
    }
}
