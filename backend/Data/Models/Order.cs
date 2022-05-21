using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double? Time { get; set; }
        public double? Amount { get; set; }
        public DateTime Date { get; set; }

        public Ticket Ticket { get; set; }
        public int EmployeeId { get; set; }
        public ApplicationUser Employee { get; set; }
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

    }
}
