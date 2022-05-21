using bitcomTickets.Data;
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
    public class UserAddValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserHelper userHelper = (UserHelper)context.ActionArguments["user"];
            var userManager = context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();
            var currentUser = userManager.GetUserAsync(context.HttpContext.User).Result;

            if (userHelper.username == null || userHelper.password == null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "username or password can not be null"
                });

            }

            var pw = new PasswordValidator<ApplicationUser>();
            if (!pw.ValidateAsync(userManager, currentUser, userHelper.password).Result.Succeeded)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Password validation errror!"
                });
            }

            ApplicationUser user = null;
            user = userManager.FindByNameAsync(userHelper.username).Result;
            if (user != null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "User already exists "
                });

            }


            user = userManager.FindByEmailAsync(userHelper.email).Result;
            if (user != null)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "email already in use"
                });

            }




        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
            
        }
    }
}