using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.EntityFrameworkCore;
using bitcomTickets.Core.Services;
using bitcomTickets.Core.Types;
using Microsoft.AspNetCore.SignalR;
using bitcomTickets.Hubs;

namespace bitcomTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {



        private Exchange _exchange;
        private ApplicationDbContext _context;
        private IOrdersRepository _ordersRepository;
        private IHubContext<ChatHub> _chatHub;
        private IHubContext<TicketHub> _ticketHub;

        public TestController(Exchange exchange, ApplicationDbContext context, IOrdersRepository ordersRepository, IHubContext<ChatHub> chatHub,IHubContext<TicketHub> ticketHub)
        {
            _exchange = exchange;
            _context = context;
            _ordersRepository = ordersRepository;
            _chatHub = chatHub;
            _ticketHub = ticketHub;
            
        }

        [HttpGet]
        
        public IActionResult GetConfig()
        {
            var email = _context.Emails.OrderBy( e => e.Id).Last();
            _exchange.CreateLocalCache(email);
            return Ok();
        }

        [HttpPost]
        [Route("test")]
        public IActionResult GetTest()
        {

            ChatMessage cm = new ChatMessage();
            cm.CreateTime = DateTime.Now;
            cm.ChatId = 3;
            cm.AuthorId = 2;
            cm.Message = "from test controllers";
            string[] users = new string[] { "2", "3" };
            _chatHub.Clients.Users(users).SendAsync("newMessage", cm).ConfigureAwait(false);

            Ticket t = new Ticket();
            t.Id = 200;
            t.Owner = _context.Users.Where(u => u.Id == 2).FirstOrDefault();
            t.Executor = _context.Users.Where( u => u.Id ==3).FirstOrDefault();
            t.Title = "LKFDSJF";
            t.State = TicketState.New;

            _ticketHub.Clients.Users(users).SendAsync("newTicket", t).ConfigureAwait(false);



            //var s = _exchange.GetServiceStatus();

            return Ok();
            
        }

    }
}
