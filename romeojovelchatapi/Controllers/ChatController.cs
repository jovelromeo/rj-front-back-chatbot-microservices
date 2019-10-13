using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using romeojovelchatapi.Domain.Models;
using romeojovelchatapi.Domain.Persistence;
using romeojovelchatapi.Domain.Services;
using romeojovelchatapi.DTOs;
using romeojovelchatapi.Extensions;
using romeojovelchatapi.Services;
using romeojovelchatapi.Services.Communication;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace romeojovelchatapi.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMessageService _messageService;
            private readonly IMapper _mapper;
        private readonly IChatBotService _chatBotService;

        public ChatController(IMessageService messageService, IMapper mapper, AppDbContext context, IChatBotService chatBotService)
        {
            _context = context;
            _messageService = messageService;
            _mapper = mapper;
            _chatBotService = chatBotService;
        }

            
        [HttpGet]
        public async  void Asend([FromQuery] string s="appl.us")
        {
            await _chatBotService.RequestMessageAsync(new MessageResponse {Code=s });
        }
    

        [HttpGet]
        public async Task<ICollection<ChatMessageResponse>> GetLastMessages()
        {
            return await _messageService.GetLastMessages();            
        }


    }

}
