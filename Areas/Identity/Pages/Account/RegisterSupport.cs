using Microsoft.AspNetCore.Identity;
using ModellenBureau.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Areas.Identity.Pages.Account
{
    public class RegisterSupport
    {
        public static async Task<IdentityResult> EnsureRole(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, AppUser user, string role)
        {
            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                _ = await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
