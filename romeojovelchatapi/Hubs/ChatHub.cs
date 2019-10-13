using Microsoft.AspNetCore.SignalR;
using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using System.Threading.Tasks;

namespace romeojovelchatapi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IChatBotService _chatBotService;

        public ChatHub(IMessageService messageService, IChatBotService chatBotService)
        {
            _messageService = messageService;
            _chatBotService = chatBotService;
        }
        public async Task SendMessage(string dsUser, string message)
        {
            long cdUser = 0;
            var result = await _messageService.ProcessMessage(message);
            if (!result.Success)
            {
                return;
            }
            switch (result.Content.MessageType)
            {
                case Enums.MessageType.MESSAGE:
                    var saveResult = await _messageService.Save(new MessageRequest(cdUser, dsUser, message));
                    if (!saveResult.Success)
                    {
                        
                    }
                    else
                    {
                        await Clients.All.SendAsync("ReceiveMessage", dsUser, message);
                    }
                    break;
                case Enums.MessageType.BOT_COMMAND:
                    await _chatBotService.RequestMessageAsync(result.Content);
                    //    await Clients.All.SendAsync("ReceiveMessage", "rjBot", chatBotMessage);
                    break;
                default:
                    break;
            }
        }

        // Call this from C#: NewsFeedHub.Static_Send(channel, content)
        public async void Send(string channel, string content)
        {
            await Clients.All.SendAsync("ReceiveMessage", channel, content);
        }
    }
}
