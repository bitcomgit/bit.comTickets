using bitcomTickets.Core.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public interface ITicketsEmailsRepository
    {
        public Ticket AddNewTicketByMail(TicketEmailHelper emailHelper, HttpContext httpContext);
        public bool IsEmail(string internetMessageId);
        public List<EmailInfo> GetEmailsInfos(int id);
        public ExchangeConnectionStatus GetExchangeStatus();
    }
}
