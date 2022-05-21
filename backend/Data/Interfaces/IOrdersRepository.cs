using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public interface IOrdersRepository
    {
        public List<Order> GetAllOrders();
        public Order AddOrder(Order order);
    }
}
