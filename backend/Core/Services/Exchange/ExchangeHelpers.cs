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
        private class CacheHelper
        {
            public CacheHelper(Email email, string rootpath)
            {
                SetProps(email, rootpath);
            }

            public CacheHelper(EmailMessage emailMessage, string rootpath)
            {
                SetProps(emailMessage, rootpath);
            }


            public string bodyPath { get; private set; } //{ return $"{path}\\bodytext.html"; } } 
            public string path { get; private set; }
            public string attachmentsPath { get; private set; }
            public string attachmentsInlinePath { get; private set; }
            public string relativePath { get; private set; }

            private void SetProps(Email email, string rootpath)
            {
                path = $"{rootpath}\\excache\\{email.FormatedInternetMessageId}";
                attachmentsPath = $"{rootpath}\\excache\\{email.FormatedInternetMessageId}\\file";
                attachmentsInlinePath = $"{rootpath}\\excache\\{email.FormatedInternetMessageId}\\inline";
                bodyPath = email.IsHtml ? $"{path}\\bodytext.html" : $"{path}\\bodytext.txt";
                relativePath = $"/excache/{email.FormatedInternetMessageId}/";
            }

            private void SetProps(EmailMessage emailMessage, string rootpath)
            {
                path = $"{rootpath}\\excache\\{emailMessage.InternetMessageId.Replace("<", "").Replace(">", "")}";
                attachmentsPath = $"{path}\\file";
                attachmentsInlinePath = $"{path}\\inline";
                bodyPath = emailMessage.Body.BodyType == BodyType.HTML ? $"{path}\\bodytext.html" : $"{path}\\bodytext.txt";
                relativePath = $"/excache/{emailMessage.InternetMessageId.Replace("<", "").Replace(">", "")}/";
            }
        }
    }
}
