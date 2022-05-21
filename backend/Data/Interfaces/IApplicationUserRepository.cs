using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace bitcomTickets.Data
{
    public interface IApplicationUserRepository
    {
        public ApplicationUser AddUser(UserHelper userHelper);
        public ApplicationUser UpdateUser(UserHelper userHelper);
        public ApplicationUser GetCurrentUser();
        public IEnumerable<ApplicationUser> GetAllWithRoles();
        public IEnumerable<ApplicationUser> GetUsersFromRole(string roleName);
        public void AddUserToRoles(ApplicationUser user, IEnumerable<string> roles);
        public void UpdatePassword(UserHelper userHelper);
        public UserConfig GetUserConfig();
        public void SetOrUpdateUserConfig(HttpContext context, UserConfig userConfig);

    }
}
