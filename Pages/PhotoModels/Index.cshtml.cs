using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModellenBureau.Authorization;
using ModellenBureau.Data;
using ModellenBureau.Models;

namespace ModellenBureau.Pages.PhotoModels
{
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(ApplicationDbContext context, 
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IList<PhotoModel> PhotoModel { get;set; }

        public async Task OnGetAsync()
        {
            var photomodels = from c in Context.PhotoModel
                           select c;

            var isAuthorized = User.IsInRole(ConRoles.ManagerRole) ||
                               User.IsInRole(ConRoles.AdministratorRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only approved contacts are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                photomodels = photomodels.Where(c => c.Status == ContactStatus.Approved
                                            || c.AppUserId == currentUserId);
            }

            PhotoModel = await photomodels.ToListAsync();
        }
    }
}
