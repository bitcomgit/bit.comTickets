using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        private IOrdersRepository _ordersRepository;


        [HttpGet]
        [Authorize(Roles = "employee")]
        public List<Order> GetAllOrders()
        {
            return _ordersRepository.GetAllOrders();
        }

        [HttpPost]
        [Authorize(Roles = "employee")]
        public Order AddOrder(Order order)
        {
            return _ordersRepository.AddOrder(order);
        }
    }
}
