
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using bitcomTickets.Data;

namespace bitcomTickets.Controllers
{
   
    [ApiController]
    public class InitController : ControllerBase
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;       
        private RoleManager<ApplicationRole> roleManager;

        private static string[] roles = { "admin", "director", "manager", "employee", "customer" };


        public InitController(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
        {
            context = _context;
            userManager = _userManager;           
            roleManager = _roleManager;
        }

        [Route("appinit")]
        public async Task<IActionResult> Initapp()
        {
            //if (context.Database.CanConnect())
            //    return BadRequest( new { status = "database alredy exist"});
            
            //context.Database.EnsureCreated();

            IdentityResult result = await userManager.CreateAsync(new ApplicationUser
            {
                UserName = "badmin",
                Email = "badmin@example.com"

            }, "U&&vs9ob");
            foreach (string role in roles)
            {
                ApplicationRole ar = new ApplicationRole { Name = role };
                await roleManager.CreateAsync(ar);

            }
            var badmin = await userManager.FindByNameAsync("badmin");
            await userManager.AddToRolesAsync(badmin, roles);
            userManager.Dispose();
            Chat chat = new Chat();
            context.Chat.Add(chat);
            context.SaveChanges();
            context.Dispose();
            return Content("Konfiguracja zaiinicjowana!", "text/html");
        }

    }
}
