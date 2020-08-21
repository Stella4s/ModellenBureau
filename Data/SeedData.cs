using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModellenBureau.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModellenBureau.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                //var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                SeedRoles(roleManager);
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            CreateRole(roleManager, ConRoles.AdministratorRole);
            CreateRole(roleManager, ConRoles.ManagerRole);
            CreateRole(roleManager, ConRoles.PhotoModelRole);
            CreateRole(roleManager, ConRoles.CustomerRole);
        }

        public static async void CreateRole (RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                _ = await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
