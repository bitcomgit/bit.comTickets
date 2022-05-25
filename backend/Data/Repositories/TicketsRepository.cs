using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Core.Types;

namespace bitcomTickets.Data
{
    public class TicketsRepository : ITicketsRepository
    {

        private ApplicationDbContext _context;

        public TicketsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public Ticket GetById(int id)
        {
            return _context.Tickets.Where(t => t.Id == id).FirstOrDefault();
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.Include(t => t.Owner).Include(t => t.Executor).Include(t => t.Contractor).ToList();
        }

        public List<Ticket> GetUserActiveTickets(HttpContext httpContext) 
        {
            var username = httpContext.User.Identity.Name;
             return _context.Tickets
                    .Include(t => t.Emails)
                    .Include(t => t.Owner)
                    .Include(t => t.Executor)
                    .Where(t => (t.Executor == null | t.Owner.UserName == username  | t.Executor.UserName == username) && t.State != TicketState.Closed)
                    .ToList();

        }

        public int CountTicketsInMonth()
        {
            Month month = new Month();
            return _context.Tickets.Where(t => t.CreatedAt >= month.FirsDayOfMonth && t.CreatedAt <= month.LastDayOfMonth).Count();
        }

        public Ticket UpdateTicket(Ticket ticket)
        {
            var _ticket = _context.Tickets
                    .Include(t => t.Owner)
                    .Include(t => t.Executor)
                    .Include(t => t.Contractor)
                    .Include(t => t.Emails).Where(t => t.Id == ticket.Id).FirstOrDefault();
            _ticket.Description = ticket.Description;
            _ticket.Title = ticket.Title;
            _ticket.State = ticket.State;
            _ticket.Executor = ticket.Executor != null ?   _context.Users.Where(u => u.Id == ticket.Executor.Id).FirstOrDefault() : null ;
            _ticket.Contractor = ticket.Contractor != null ? _context.Contractors.Where(c => c.Id == ticket.Contractor.Id).FirstOrDefault() : null;
            _context.SaveChanges();
            return _ticket;
        }

        public List<Order> GetTicketOrders(int id)
        {
            return _context.Orders.Where(o => o.Ticket.Id == id).ToList();
        }
    }
}
