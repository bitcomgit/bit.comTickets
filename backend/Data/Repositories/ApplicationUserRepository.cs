using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcomTickets.Data
{
    public class ApplicationUserRepository : IApplicationUserRepository,IDisposable
    {
        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _userManager = userManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private IHttpContextAccessor _httpContextAccessor;


        public ApplicationUser GetCurrentUser()
        {
            var user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            if (user != null)
                user.Roles = _userManager.GetRolesAsync(user).Result;
            return user;
        }

        public ApplicationUser AddUser(UserHelper userHelper)
        {
            IdentityResult result = _userManager.CreateAsync(new ApplicationUser
            {
                UserName = userHelper.username,
                Email = userHelper.email,
                FirstName = userHelper.firstname,
                LastName = userHelper.lastname,

            }, userHelper.password).Result;

            ApplicationUser createdUser = (result.Succeeded)? _userManager.FindByNameAsync(userHelper.username).Result:null;
            if (createdUser != null  && userHelper.roles != null)
                AddUserToRoles(createdUser, userHelper.roles);
            createdUser.Roles = _userManager.GetRolesAsync(createdUser).Result;
            return createdUser;
        }


        public ApplicationUser UpdateUser(UserHelper userHelper)
        {
            var user = _userManager.FindByIdAsync(userHelper.id.ToString()).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            user.LastName = userHelper.lastname;
            user.FirstName = userHelper.firstname;
            user.Email = userHelper.email;
            var identity = _userManager.RemoveFromRolesAsync(user, roles).Result;
            identity = _userManager.AddToRolesAsync(user, userHelper.roles).Result;
            identity = _userManager.UpdateAsync(user).Result;
            user.Roles = _userManager.GetRolesAsync(user).Result;
            return user;
        }

        public void AddUserToRoles(ApplicationUser user, IEnumerable<string> roles)
        {
            var u = _userManager.AddToRolesAsync(user, roles).Result;
        }

        public void UpdatePassword(UserHelper userHelper)
        {
            var user = _userManager.FindByNameAsync(userHelper.username).Result;
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var result =  _userManager.ResetPasswordAsync(user, token, userHelper.password).Result;
        }

        public IEnumerable<ApplicationUser> GetAllWithRoles()
        {
            var users = _userManager.Users.ToList();
            foreach (ApplicationUser user in users)
                user.Roles = _userManager.GetRolesAsync(user).Result;
            return users;
        }

        public IEnumerable<ApplicationUser> GetUsersFromRole(string roleName)
        {
            return  _userManager.GetUsersInRoleAsync(roleName).Result.ToList().Where(u => u.Id != 1).ToList();
        }

        public UserConfig GetUserConfig() 
        {
            var user = GetCurrentUser();
            return _context.UsersConfigs.Where(u => u.User == user).FirstOrDefault();
            
        }

        public void SetOrUpdateUserConfig(HttpContext httpContext, UserConfig userConfig)
        {
            var user = _context.Users.Include(u => u.UserConfig).Where(u => u.UserName == httpContext.User.Identity.Name).FirstOrDefault();
            user.UserConfig = userConfig;
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _context.Dispose();
        }
    }
}
