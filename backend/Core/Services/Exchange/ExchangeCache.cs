using bitcomTickets.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IO;

namespace bitcomTickets.Core.Services
{
    public partial class Exchange
    {

        public void CreateLocalCache(Email email)
        {
            CacheHelper helper = new CacheHelper(email, _environment.WebRootPath);
            EmailMessage exchangeMail = EmailMessage.Bind(Service, new ItemId(email.ExchangeItemId));
            CreateCacheDirs(helper);
            SaveBodyAndAttachments(exchangeMail, helper);
        }

        public void CreateLocalCache(EmailMessage emailMessage)
        {
            CacheHelper helper = new CacheHelper(emailMessage, _environment.WebRootPath);
            CreateCacheDirs(helper);
            SaveBodyAndAttachments(emailMessage, helper);
        }

        private void SaveBodyAndAttachments(EmailMessage emailMessage, CacheHelper helper)
        {
            if (emailMessage.Body.BodyType == BodyType.Text)
            {
                File.WriteAllText(helper.bodyPath, emailMessage.Body.Text);
            }

            string htmlBody = emailMessage.Body.Text;

            foreach (var att in emailMessage.Attachments)
            {

                if (att.IsInline)
                {
                    FileAttachment fileAttachment = att as FileAttachment;
                    string name = fileAttachment.ContentId + fileAttachment.Name;
                    fileAttachment.Load($"{helper.attachmentsInlinePath}\\{name}");
                    htmlBody = htmlBody.Replace("cid:" + fileAttachment.ContentId, helper.relativePath + "inline/" + name);

                }
                else
                {
                    FileAttachment fileAttachment = att as FileAttachment;
                    fileAttachment.Load($"{helper.attachmentsPath}\\{fileAttachment.Name}");
                }

            }
            File.WriteAllText(helper.bodyPath, htmlBody);
        }

        private void CreateCacheDirs(CacheHelper helper)
        {
            if (!Directory.Exists(helper.path))
                Directory.CreateDirectory(helper.path);
            if (!Directory.Exists(helper.attachmentsPath))
                Directory.CreateDirectory(helper.attachmentsPath);
            if (!Directory.Exists(helper.attachmentsInlinePath))
                Directory.CreateDirectory(helper.attachmentsInlinePath);

        }




    }
}
