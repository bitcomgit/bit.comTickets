using bitcomTickets.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Core.Types
{
    public class EmailInfo
    {
        public EmailInfo(Email email) 
        {
            SetProps(email);
        }
        public int id { get; private set; }
        public string subject { get; private set; }
        public string from { get; private set; }
        public string bodyPath { get; private set; }
        public List<string> attachments { get; private set; }

        private void SetProps(Email email) 
        {
            id = email.Id;
            bodyPath = (email.IsHtml) ? $"excache/{email.FormatedInternetMessageId}/bodytext.html" : $"excache/{email.FormatedInternetMessageId}/bodytext.txt";
            from = email.From;
            subject = email.Subject;
            attachments = new List<string>();
            foreach (string attachmentName in email.AttachmentsList)
            {
                if (string.IsNullOrEmpty(attachmentName)) continue;
                attachments.Add($"excache/{email.FormatedInternetMessageId}/file/{attachmentName}");
            }
            
        }

    }
}
