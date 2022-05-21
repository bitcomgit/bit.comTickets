using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class Chat
    {
        public int Id { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return (this.Id == 1) ? "bitcom" : string.Empty;
            }
        }
        public List<ChatMessage> Messages { get; set; }
        public List<ApplicationUser> Members { get; set; }
    }
}
