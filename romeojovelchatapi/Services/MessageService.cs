using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Repositories;
using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Enums;
using romeojovelchatapi.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Services
{
    public class MessageService : IMessageService
    {
        public static string STOCK_COMMAND = "/stock";
        
        private readonly ITbMessageRepository _repository;

        public MessageService(ITbMessageRepository tbMessageRepository)
        {
            _repository = tbMessageRepository;
        }

        public async Task<ICollection<ChatMessageResponse>> GetLastMessages()
        {
            var messages =  await _repository.GetLastMessages(50);
            return (from x in messages select new ChatMessageResponse { message = x.DsMessage, userId=x.CdUser }).ToList();
        }

        public async Task<MessageResult> ProcessMessage(string message)
        {
            await Task.Yield();
            if (message == null)
            {
                return new MessageResult("Message is empty.");
            }
            //check if contains /stock
            var okMessage = message.Trim();
            if (!okMessage.ToLower().StartsWith(STOCK_COMMAND))
            {
                return new MessageResult(new MessageResponse
                {
                    Message = okMessage,
                    MessageType = Enums.MessageType.MESSAGE
                });
            }
            okMessage = okMessage.Substring(STOCK_COMMAND.Length).Trim();
            if (!okMessage.StartsWith("="))
            {
                return new MessageResult("Wrong command format.");
            }
            okMessage = okMessage.Substring(1).Trim();
            if (string.IsNullOrEmpty(okMessage))
            {
                return new MessageResult("No command value.");
            }
            
            return new MessageResult(new MessageResponse
            {
                MessageType = Enums.MessageType.BOT_COMMAND,
                Code = okMessage
            });

        }

        public async Task<MessageResult> Save(MessageRequest messageRequest)
        {
            var guid = Guid.NewGuid().ToString();
            var tb = new TbChatMessage { CdChatMessage=guid, CdUser = messageRequest.UserId, DsMessage = messageRequest.Message };
            try
            {
            await _repository.SaveAsync(tb);
            await _repository.SaveChagesAsync();
            return new MessageResult(new MessageResponse {Message=messageRequest.Message,MessageType=MessageType.MESSAGE  });
            }
            catch (Exception e)
            {
                return new MessageResult($"Message could not be saved. {e.Message}");
            }
        }
    }
}
