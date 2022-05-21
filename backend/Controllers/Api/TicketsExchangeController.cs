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



        public TicketsExchangeController(ITicketsEmailsRepository emailRepository, IHubContext<TicketHub> ticketHub)
        {
            _emailRepository = emailRepository;
            _ticketHub = ticketHub;
        }

        private ITicketsEmailsRepository _emailRepository;
        private IHubContext<TicketHub> _ticketHub;

        [HttpPost]
        [Authorize( Roles ="employee")]
        public IActionResult AddNewTicket(TicketEmailHelper emailHelper) 
        {
            var ticket = _emailRepository.AddNewTicketByMail(emailHelper,HttpContext);
            var owner = ticket.Owner.Id.ToString();
            var executor = ticket?.Executor?.Id.ToString();
            var users = new string[] { owner, executor }.ToList().Distinct().ToList();
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

    }
}
