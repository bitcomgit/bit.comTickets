using bitcomTickets.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core.Filters
{
    public class UserUpdatePassordFilter : ActionFilterAttribute
    {

        private UserManager<ApplicationUser> userManager;
       
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            this.userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var currentUser = userManager.GetUserAsync(context.HttpContext.User).Result;
            var pw = new PasswordValidator<ApplicationUser>();
            UserHelper userHelper = (UserHelper)context.ActionArguments["user"];

            if (!CanChangePassword(currentUser, userHelper))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Invalid credentials!"
                });
                return;
            }


            if (string.IsNullOrEmpty(userHelper.password) | string.IsNullOrEmpty(userHelper.username))
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Username or password can not be null"
                });
                return;
            }
           
            if (!pw.ValidateAsync(userManager, currentUser, userHelper.password).Result.Succeeded)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Password validation errror!"
                });
                return;
            }



            
            

        }

        private bool CanChangePassword(ApplicationUser currentUser, UserHelper user)
        {
            bool resut = false;
            if (currentUser.UserName == user.username)
                return userManager.CheckPasswordAsync(currentUser, user.oldPassword).Result;

            if (userManager.IsInRoleAsync(currentUser, "admin").Result)
                return true;

            return resut;
        }
    }
}
