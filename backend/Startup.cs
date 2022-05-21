using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Design;

using bitcomTickets.Data;
using bitcomTickets.Core.Middleware;
using bitcomTickets.Hubs;
using Newtonsoft.Json.Serialization;
using bitcomTickets.Core.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace bitcomTickets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private IConfiguration configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            }).AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
            );

            services.AddDbContext<ApplicationDbContext>(
               options =>
               options.UseSqlServer(configuration.GetConnectionString("AppDbContext")
               )
               //.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
               );

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();

            services.AddSignalR().AddNewtonsoftJsonProtocol(x => x.PayloadSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);


            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<ITicketsEmailsRepository, TicketsEmailsRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
            services.AddScoped<IContractorsRepository, ContracotrsRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            
            services.AddScoped<Exchange>();

            //TODO: remove from prod
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.ExpireTimeSpan = TimeSpan.FromSeconds(3600);
               
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<ChatHub>("/chat");
                    endpoints.MapHub<TicketHub>("/ticket");
                });

            app.UseStaticFiles();


            app.UseSpa(builder =>
            {
                if (env.IsDevelopment())
                {
                   builder.UseProxyToSpaDevelopmentServer("http://localhost:8888");
                }
            });
        }
    }
}
