using Microsoft.Exchange.WebServices.Data;
using System.Linq;

namespace bitcomTickets.Core.Services
{
    public partial class Exchange
    {
        public EmailMessage FindByInternetMessageId(string internetMessageId)
        {
            EmailMessage email = null;
            ItemView view = new ItemView(1);
            SearchFilter.IsEqualTo filter = new SearchFilter.IsEqualTo(EmailMessageSchema.InternetMessageId, internetMessageId);

            FolderId mailbox = new FolderId(WellKnownFolderName.Inbox);
            FindItemsResults<Item> findResults = Service.FindItems(mailbox, filter, view);
            email = (EmailMessage)findResults.FirstOrDefault();
            if (email != null)
                return email;

            foreach (string mailboxName in _userConfig.SharedMailBoxes)
            {
                mailbox = new FolderId(WellKnownFolderName.Inbox, mailboxName);
                findResults = Service.FindItems(mailbox, filter, view);
                email = (EmailMessage)findResults.FirstOrDefault();
                if (email != null)
                    return email;
            }

            return email;
        }
    }
}
