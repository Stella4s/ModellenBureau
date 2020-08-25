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
    public class DeleteModel : DI_BasePageModel
    {
        public DeleteModel(ApplicationDbContext context,
        IAuthorizationService authorizationService,
        UserManager<AppUser> userManager)
        : base(context, authorizationService, userManager)
        {
        }

        [BindProperty]
        public PhotoModel PhotoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PhotoModel = await Context.PhotoModel.FirstOrDefaultAsync(m => m.Id == id);

            if (PhotoModel == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, PhotoModel,
                                                 AuthorizeOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            PhotoModel = await Context.PhotoModel.AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id== id);

            if (PhotoModel == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, PhotoModel,
                                                 AuthorizeOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.PhotoModel.Remove(PhotoModel);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
