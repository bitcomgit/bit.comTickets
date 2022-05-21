using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using bitcomTickets.Data;
using Microsoft.AspNetCore.Authorization;

namespace bitcomTickets.Hubs
{
    public class TicketHub : Hub
    {
        //[Authorize(Roles = "employee")]
        //public async Task SendMessage(Ticket ticket)
        //{
        //    var owner = ticket.Owner.Id.ToString();
        //    var executor = ticket?.Executor.Id.ToString();
        //    var users = new string[] { owner, executor }.ToList().Distinct().ToList();
        //    await Clients.Users(users).SendAsync("newTicket",ticket);
        //}
    }
}
