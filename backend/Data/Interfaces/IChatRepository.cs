using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace bitcomTickets.Data
{ 
    public interface IChatRepository
    {
        public void AddUserToChat(UserHelper userHelper);
        public IEnumerable<Chat> GetUserChats(HttpContext httpContext);
        public Task<IReadOnlyList<string>> AddMessage(ChatMessage message);
        //public IEnumerable<ChatMessage> GetAllChatMessages(int id);
        //public Chat GetChatWithMessages(int id);
        //public IEnumerable<Chat> GetUserChats(ApplicationUser user);
        //public void SetMessagesRead(ApplicationUser user, int id);
    }
}
