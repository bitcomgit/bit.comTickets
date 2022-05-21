using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ReadTime { get; set; }
        
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
