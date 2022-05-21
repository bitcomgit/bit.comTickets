using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using bitcomTickets.Data;
using bitcomTickets.Core.Filters;


namespace bitcomTickets.Controllers
{
    
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public AuthenticationController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        private SignInManager<ApplicationUser> _signInManager;


        [HttpPost]        
        [Route("auth/login")]
        [UserLoginNullValuesFilter]
        public  async Task<IActionResult> Login(UserHelper userHelper)
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false) ;
            var result = _signInManager.PasswordSignInAsync(userHelper.username, userHelper.password, false, false).Result;
            if (result.Succeeded)
                return Ok();
            return Unauthorized();
        }

        [HttpGet]
        [Route("auth/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

    }
}
