using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace bitcomTickets.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
       
        private IChatRepository _chatRepository { get; set;  }
        
        [Authorize(Roles = "employee")]
        public async Task SendMessage(ChatMessage message)
        {
            var users = _chatRepository.AddMessage(message).Result;
            await Clients.Users(users).SendAsync("newMessage", message);
        }
    }
}
