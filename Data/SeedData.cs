using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModellenBureau.Authorization;
using ModellenBureau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
            await EnsureRole(serviceProvider, adminID, ConRoles.AdministratorRole);

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await SeedRolesAsync(roleManager);
        }

        #region Seeding Roles
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            await CreateRole(roleManager, ConRoles.AdministratorRole);
            await CreateRole(roleManager, ConRoles.ManagerRole);
            await CreateRole(roleManager, ConRoles.PhotoModelRole);
            await CreateRole(roleManager, ConRoles.CustomerRole);
        }

        public static async Task CreateRole (RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                _ = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        #endregion

        #region seeding Admin
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                            string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = UserName,
                    Email = "admin@contoso.com",
                    EmailConfirmed = true,
                    FirstName = "Brutus",
                    LastName = "Romanus",
                    Address = "1234 Main St",
                    City = "Rome"
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                                      string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        #endregion
      
    }
}
