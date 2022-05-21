using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Mvc;

namespace bitcomTickets.Core.Filters
{
    public class UserLoginNullValuesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            UserHelper userHelper = (UserHelper)context.ActionArguments["userHelper"];
          
            if (userHelper.username == null | userHelper.password == null)
            {
                context.Result = new BadRequestObjectResult(new
                {                    
                    message = "username or password can not be null"
                });
            }
        }
    }
}
