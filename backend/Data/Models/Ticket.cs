using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using bitcomTickets.Core.Types;

namespace bitcomTickets.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public TicketState State { get; set; }
        public Contractor Contractor { get; set; }
        public string Number { get; set; }
        public string ContractorNumber { get; set; }
        public ApplicationUser Owner { get; set; }
        public ApplicationUser Executor { get; set; }
        public List<Email> Emails { get; set; }
        public List<Order> Orders { get; set; }  

    }
}
