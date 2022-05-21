using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class ApplicationUser : IdentityUser<int>
    {
        public override int Id { get; set; }
        
        [JsonProperty("username")]
        public override string UserName { get; set; }
        
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        
        [JsonProperty("firstname")]        
        public string FirstName { get; set; }
        
        public override string Email { get; set; }
        
        [JsonProperty("phonenumber")]
        public override string PhoneNumber { get; set; }

        [JsonIgnore]
        public List<Chat> Chats { get; set; }

        [JsonIgnore]
        public virtual UserConfig UserConfig { get; set; }


        #region JsonIgnore
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [JsonIgnore]
        public override string NormalizedEmail { get; set; }
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }

        #endregion

        [NotMapped]
        public IList<string> Roles { get; set; }
    }
}
