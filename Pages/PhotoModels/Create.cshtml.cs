using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModellenBureau.Authorization;
using ModellenBureau.Data;
using ModellenBureau.Models;

namespace ModellenBureau.Pages.PhotoModels
{
    public class CreateModel : DI_BasePageModel
    {
        public CreateModel(ApplicationDbContext context, 
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PhotoModel PhotoModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            PhotoModel.AppUserId = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                User, PhotoModel,
                                                AuthorizeOperations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.PhotoModel.Add(PhotoModel);
            PhotoModel.User = await UserManager.FindByIdAsync(PhotoModel.AppUserId);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
