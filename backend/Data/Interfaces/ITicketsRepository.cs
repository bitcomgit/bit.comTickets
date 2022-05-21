using bitcomTickets.Core.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public interface ITicketsRepository
    {
        public Ticket GetById(int id);
        public List<Ticket> GetAll();
        public List<Ticket> GetUserActiveTickets(HttpContext httpContext);
        public int CountTicketsInMonth();
        public Ticket UpdateTicket(Ticket ticket);
        public List<Order> GetTicketOrders(int id);
    }
}
