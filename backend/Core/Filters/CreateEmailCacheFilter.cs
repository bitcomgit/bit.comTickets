using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core.Filters
{
    public class CreateEmailCacheFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {

            //TODO: WTF?
            base.OnActionExecuted(context);
            context.HttpContext.Response.OnCompleted(() =>
            {
                Task.Run(() => 
                {

                    for (int i = 0; i < 100; i++)
                    {
                        
                            for (int ii = 0; ii < 100; ii++)
                            {
                                Console.WriteLine("sdfdf");
                            }
                        
                    }
                    Console.WriteLine("end");

                });
                
                return Task.CompletedTask;
            });


        }

    }
}
