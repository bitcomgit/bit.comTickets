using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Core.Types;
using bitcomTickets.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Exchange.WebServices.Data;


namespace bitcomTickets.Data
{
    public class TicketsEmailsRepository : ITicketsEmailsRepository
    {
        public TicketsEmailsRepository(ApplicationDbContext context, Exchange exchange, IWebHostEnvironment environment)
        {
            _context = context;
            _exchange = exchange;
            _environment = environment;

        }
        private Exchange _exchange;
        private ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public Ticket AddNewTicketByMail(TicketEmailHelper emailHelper, HttpContext httpContext)
        {
            EmailMessage exchangeMail = _exchange.FindByInternetMessageId(emailHelper.InternetMessageId);
            exchangeMail.Load();

            Email email = new Email();
            email.ConversationId = exchangeMail.ConversationId;
            email.InternetMessageId = exchangeMail.InternetMessageId;
            email.ExchangeItemId = exchangeMail.Id.UniqueId;
            email.IsHtml = exchangeMail.Body.BodyType == BodyType.HTML;
            email.From = exchangeMail.From.Address;
            email.Subject = exchangeMail.Subject;
            email.FileAttachments = string.Join("|", exchangeMail.Attachments.Cast<FileAttachment>().Where(a => !a.IsInline).Select(a => a.Name).ToList());

            Ticket ticket = new Ticket();
            ticket.Title = exchangeMail.Subject;
            ticket.CreatedAt = DateTime.Now;
            ticket.Owner = _context.Users.Where(u => u.UserName == httpContext.User.Identity.Name).FirstOrDefault();
            ticket.Executor = _context.Users.Where(u => u.Id == emailHelper.UserId).FirstOrDefault();
            ticket.State = TicketState.New;
            ticket.Number = GetNumber();

            ticket.Emails = new List<Email>();
            ticket.Emails.Add(email);

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            _exchange.CreateLocalCache(exchangeMail);

            return ticket;
        }

        public List<EmailInfo> GetEmailsInfos(int id)
        {
            var emails = _context.Emails.Where(e => e.TicketId == id).ToList();
            List<EmailInfo> list = new List<EmailInfo>();
            foreach (Email email in emails)
            {
                if (!Directory.Exists($"{_environment.WebRootPath}\\excache\\{email.FormatedInternetMessageId}"))
                    _exchange.CreateLocalCache(email);
                list.Add(new EmailInfo(email));
            }
            return list;
        }
        public ExchangeConnectionStatus GetExchangeStatus()
        {
            return _exchange.GetServiceStatus();
        }

        private string GetNumber()
        {
            string result = string.Empty;
            bitcomTickets.Core.Types.Month month = new bitcomTickets.Core.Types.Month();
            int total = _context.Tickets.Where(t => t.CreatedAt >= month.FirsDayOfMonth && t.CreatedAt <= month.LastDayOfMonth).Count();
            result = $"ZG/{month.FirsDayOfMonth.Year}/{month.FirsDayOfMonth.Month}/{total + 1}";
            return result;
        }

      
    }
}
