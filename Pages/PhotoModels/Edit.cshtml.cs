using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModellenBureau.Authorization;
using ModellenBureau.Data;
using ModellenBureau.Models;

namespace ModellenBureau.Pages.PhotoModels
{
    public class EditModel : DI_BasePageModel
    {

        public EditModel(ApplicationDbContext context,
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
                                                  AuthorizeOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch Contact from DB to get OwnerID.
            var photomodel = await Context
                .PhotoModel.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (photomodel == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                User, PhotoModel,
                                                AuthorizeOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            PhotoModel.AppUserId = photomodel.AppUserId;

            Context.Attach(PhotoModel).State = EntityState.Modified;

            if (PhotoModel.Status == ContactStatus.Approved)
            {
                // If the contact is updated after approval, 
                // and the user cannot approve,
                // set the status back to submitted so the update can be
                // checked and approved.
                var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                        PhotoModel,
                                        AuthorizeOperations.Approve);

                if (!canApprove.Succeeded)
                {
                    PhotoModel.Status = ContactStatus.Submitted;
                }
            }

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoModelExists(PhotoModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PhotoModelExists(int id)
        {
            return Context.PhotoModel.Any(e => e.Id == id);
        }
    }
}
