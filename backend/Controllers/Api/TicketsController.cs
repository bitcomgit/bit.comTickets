using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;
using bitcomTickets.Core.Types;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        public TicketsController(ITicketsRepository ticketsRepository, ITicketsEmailsRepository ticketsEmailsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _ticketsEmailsRepository = ticketsEmailsRepository;
        }

        private ITicketsRepository _ticketsRepository;
        private ITicketsEmailsRepository _ticketsEmailsRepository;
        
        [HttpGet]
        [Authorize(Roles = "employee")]
        public List<Ticket> GetAllTickets()
        {
            return _ticketsRepository.GetAll();
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        [Route("active")]
        public List<Ticket> GetAllActive()
        {
            return _ticketsRepository.GetUserActiveTickets(HttpContext);
        }



        [HttpGet]
        [Authorize(Roles = "employee")]
        [Route("{id}")]
        public Ticket GetById(int id)
        {
            return _ticketsRepository.GetById(id);
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        [Route("{id}/emails")]
        public List<EmailInfo> GetEmailsInfos(int id)
        {
            return _ticketsEmailsRepository.GetEmailsInfos(id);
        }

        [HttpGet]
        [Authorize(Roles = "employee")]
        [Route("{id}/orders")]
        public List<Order> GetOrders(int id)
        {
            return _ticketsRepository.GetTicketOrders(id);
        }



        [HttpPut]
        [Authorize(Roles = "employee")]

        public Ticket UpdateTicet(Ticket ticket)
        {
            return _ticketsRepository.UpdateTicket(ticket);
        }
    }
}
