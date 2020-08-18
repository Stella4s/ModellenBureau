using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModellenBureau.Data;
using ModellenBureau.Models;

namespace ModellenBureau.Pages.PhotoModels
{
    public class CreateModel : PageModel
    {
        private readonly ModellenBureau.Data.ApplicationDbContext _context;

        public CreateModel(ModellenBureau.Data.ApplicationDbContext context)
        {
            _context = context;
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

            _context.PhotoModel.Add(PhotoModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
