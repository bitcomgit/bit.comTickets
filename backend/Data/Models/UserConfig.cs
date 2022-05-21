using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Exchange.WebServices.Data;
using bitcomTickets.Core.Tools;
namespace bitcomTickets.Data
{
    public class UserConfig
    {
        public int Id { get; set; }
        public string ep { get; set; }
        public string eu { get; set; }
        public string mb { get; set; }

        public int UserId { get; set; }
        public  ApplicationUser User { get; set; }


        #region NotMapped

        [NotMapped]
        public string eph 
        {
            get
            {
                return bcEncrypt.DecryptStringToString(ep);
            }
            set 
            {
                ep = bcEncrypt.EncryptStringToString(value);
            }
        }
        [NotMapped]
        public string euh 
        {
            get
            {
                return bcEncrypt.DecryptStringToString(eu); ;
            }
            set 
            {
                eu = bcEncrypt.EncryptStringToString(value);
            }
        }

        
        [NotMapped]
        public WebCredentials Credentials
        {
            get
            {
                return new WebCredentials(euh, eph);
            }
        }
        [NotMapped]
        public List<string> SharedMailBoxes
        {
            get
            {
                List<string> pomoc = new List<string>();
                pomoc.Add("pomoc@bitcom.com.pl");

                if (mb == null) 
                    return pomoc;
                pomoc.AddRange(new List<string>(mb.Split(new string[] { "|" }, StringSplitOptions.None)));
                return pomoc;
            }
            set
            {
                var list = value;
                if (!value.Contains("pomoc@bitcom.com.pl"))
                    list.Add("pomoc@bitcom.com.pl");
                mb = string.Join("|",list);
            }
        }
        #endregion


    }
}
