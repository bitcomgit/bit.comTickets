using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace bitcomTickets.Data
{
    public class OrdersRepository : IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        private ApplicationDbContext _context;


        public Order AddOrder(Order order)
        {

            Order _order = new Order();
            _order.Employee = _context.Users.Where(u => u.Id == order.Employee.Id).FirstOrDefault();
            _order.Contractor = _context.Contractors.Where(c => c.Id == order.Contractor.Id).FirstOrDefault();
            _order.Ticket = _context.Tickets.Where(t => t.Id == order.Ticket.Id).FirstOrDefault();
            _order.Description = order.Description;
            _order.Time = order.Time;
            _order.Amount = order.Amount;
            _order.Date = DateTime.Now;

            _context.Orders.Add(_order);
            _context.SaveChanges();

            return _order;
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }
    }
}
