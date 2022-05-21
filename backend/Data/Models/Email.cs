using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class Email
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string InternetMessageId { get; set; }
        [JsonIgnore]
        public string ConversationId { get; set; }
        [JsonIgnore]
        public string ExchangeItemId { get; set; }
        public string From { get; set; }
        [JsonIgnore]
        public string Subject { get; set; }
        [JsonIgnore]
        public string FileAttachments { get; set; }
        [JsonIgnore]
        public bool IsHtml { get; set; }

        [JsonIgnore]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }


        [JsonIgnore]
        [NotMapped]
        public string FormatedInternetMessageId
        {
            get
            {
                return InternetMessageId.Replace("<","").Replace(">","");
            }
        }
        [JsonIgnore]
        [NotMapped]
        public List<string> AttachmentsList 
        {
            get 
            {
                return new List<string>(FileAttachments.Split(new string[] { "|" }, StringSplitOptions.None));
            }
        }

    }
}
