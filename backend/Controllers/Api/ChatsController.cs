using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using bitcomTickets.Hubs;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        public ChatsController(IChatRepository _chatRepository)
        {
            chatRepository = _chatRepository;
            
        }
        private IChatRepository chatRepository;



        [HttpPost]
        [Route("adduser")]
        [Authorize(Roles = "admin")]
        public IActionResult AddUser(UserHelper user)
        {
            chatRepository.AddUserToChat(user);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        public IEnumerable<Chat> GetUserChats()
        {
            return chatRepository.GetUserChats(HttpContext);
            //return new List<Chat>();
        }

    }
}
