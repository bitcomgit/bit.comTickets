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

namespace bitcomTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {



        private Exchange _exchange;
        private ApplicationDbContext _context;
        private IOrdersRepository _ordersRepository;

        public TestController(Exchange exchange, ApplicationDbContext context, IOrdersRepository ordersRepository)
        {
            _exchange = exchange;
            _context = context;
            _ordersRepository = ordersRepository;
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

            var x = "Dupa";
            Console.WriteLine(x);


            //var s = _exchange.GetServiceStatus();

            return Ok();
            
        }

    }
}
