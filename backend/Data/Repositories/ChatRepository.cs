using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bitcomTickets.Data
{
    public class ChatRepository : IChatRepository
    {
        public ChatRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IApplicationUserRepository usersRepository)
        {
            this._context = context;
            this._userManager = userManager;
            this._usersRepository = usersRepository;
        }
        
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _usersRepository;

        public void AddUserToChat(UserHelper userHelper)
        {
            var employees = _usersRepository.GetUsersFromRole("employee").Where(e => e.Id != userHelper.id && e.Id != 1).ToList(); 
            var user = _context.Users.Where(u => u.Id == userHelper.id).FirstOrDefault();
          ;
            Chat bitcom = _context.Chat.Include(c => c.Members).Where(cc => cc.Id == 1).FirstOrDefault();
            
            if (!bitcom.Members.Contains(user))
                bitcom.Members.Add(user);

            foreach (var employee in employees)
            {
                var chatHelper = _context.Chat.Include(c => c.Members)
                    .Where(cc => cc.Members.Count == 2 && cc.Members.Contains(user) && cc.Members.Contains(employee))
                    .FirstOrDefault();

                if (chatHelper == null)
                {
                    Chat chat = new Chat();
                    chat.Members = new List<ApplicationUser>();
                    chat.Members.Add(user);
                    chat.Members.Add(employee);
                    _context.Chat.Add(chat);
                }
            }

            _context.SaveChanges();
        }

        public IEnumerable<Chat> GetUserChats(HttpContext httpContext)
        {
            var user = _userManager.GetUserAsync(httpContext.User).Result;
            return _context.Chat.Include(c => c.Members).Include(c => c.Messages).AsSingleQuery().Where(c => c.Members.Contains(user)).ToList(); ;
        }



        public  async Task<IReadOnlyList<string>> AddMessage(ChatMessage message)
        {
            message.CreateTime = DateTime.Now;
            message.Author = _context.Users.Where(u => u.Id == message.AuthorId).FirstOrDefault();
            message.Chat = _context.Chat.Where(c => c.Id == message.ChatId).FirstOrDefault();
            await _context.ChatMessage.AddAsync(message).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            
            Chat chat = _context.Chat.Where(c => c.Id == message.Chat.Id).Include(c => c.Members).FirstOrDefault();
            return chat.Members.Select(m => m.Id.ToString()).ToList();
            
        }

        //public IEnumerable<ChatMessage> GetAllChatMessages(int id)
        //{
        //    return _context.ChatMessage.Where(m => m.ChatId == id).ToList();
        //}

        //public Chat GetChatWithMessages(int id) 
        //{
        //    return _context.Chat.Include(c => c.Members).Include(c => c.Messages).Where(c => c.Id == id).FirstOrDefault();
        //}

        //public IEnumerable<Chat> GetUserChats(ApplicationUser user)
        //{
        //    return _context.Chat.Include(c => c.Members).Include(c => c.Messages).Where(c => c.Members.Contains(user)).ToList();
        //}

        //public void SetMessagesRead(ApplicationUser user, int id)
        //{
        //    var messages = _context.ChatMessage.Where(m => m.AuthorId != user.Id && m.ChatId == id);
        //    foreach (var message in messages)
        //        message.IsRead = true;
        //    _context.SaveChanges();
        //}
    }
}