using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;
using bitcomTickets.Core.Filters;

namespace bitcomTickets.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController(IApplicationUserRepository _usersRepository)
        {
            usersRepository = _usersRepository;
        }
        private IApplicationUserRepository usersRepository;


        [HttpGet]
        [Authorize(Roles = "admin")]
        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return usersRepository.GetAllWithRoles();
        }


        [HttpGet]
        [Route("currentuser")]
        [Authorize]
        public ApplicationUser GetCurentUser()
        {
            return usersRepository.GetCurrentUser();
        }

        [HttpGet]
        [Route("employees")]
        [Authorize(Roles = "employee")]
        public IEnumerable<ApplicationUser> GetEmployees()
        {
            return usersRepository.GetUsersFromRole("employee");
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        [UserAddValidationFilter]
        public ApplicationUser AddUser(UserHelper user)
        {
            
            return usersRepository.AddUser(user);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public ApplicationUser UpdateUser(UserHelper user)
        {
            return usersRepository.UpdateUser(user);
        }


        [HttpPost]
        [Route("password")]
        [Authorize]
        [UserUpdatePassordFilter]
        public IActionResult UpdateUserPassword(UserHelper user)
        {
            usersRepository.UpdatePassword(user);
            return Ok();
        }

        [HttpPost]
        [Route("config")]
        [Authorize(Roles ="employee")]
        
        public IActionResult UpdateUserConfig(UserConfig config)
        {
            usersRepository.SetOrUpdateUserConfig(HttpContext, config);
            return Ok();
        }

    }
}
