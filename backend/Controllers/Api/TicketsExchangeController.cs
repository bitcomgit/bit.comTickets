using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using bitcomTickets.Core.Filters;
using Microsoft.AspNetCore.SignalR;
using bitcomTickets.Hubs;
using Microsoft.AspNetCore.Authorization;
using bitcomTickets.Core.Types;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/emails")]
    [ApiController]
    public class TicketsExchangeController : ControllerBase
    {

        public TicketsExchangeController(ITicketsEmailsRepository emailRepository, IHubContext<TicketHub> ticketHub, ApplicationDbContext context)
        {
            _emailRepository = emailRepository;
            _ticketHub = ticketHub;
            _context = context;

        }
        private ApplicationDbContext _context;
        private ITicketsEmailsRepository _emailRepository;
        private IHubContext<TicketHub> _ticketHub;

        [HttpPost]
        [Authorize( Roles ="employee")]
        public IActionResult AddNewTicket(TicketEmailHelper emailHelper) 
        {
            var ticket = _emailRepository.AddNewTicketByMail(emailHelper,HttpContext);
            var owner = ticket.Owner.Id.ToString();
            var executor = ticket?.Executor?.Id.ToString();
            string[] users = new string[] { owner, executor };

            _ticketHub.Clients.Users(users).SendAsync("newTicket", ticket);
            return Ok();
        }

        [HttpGet]
        [Route("status")]
        [Authorize(Roles = "employee")]
        public ExchangeConnectionStatus GetConnectionStatus() 
        {
            return _emailRepository.GetExchangeStatus();
        }

        [HttpPost]
        [Route("checkemail")]
        [Authorize(Roles = "employee")]
        public bool IsEmailInSystem(TicketEmailHelper helper)
        {
            return _emailRepository.IsEmail(helper.InternetMessageId);
        }

    }
}
