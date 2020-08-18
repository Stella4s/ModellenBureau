using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModellenBureau.Data;
using ModellenBureau.Models;

namespace ModellenBureau.Pages.PhotoModels
{
    public class DeleteModel : PageModel
    {
        private readonly ModellenBureau.Data.ApplicationDbContext _context;

        public DeleteModel(ModellenBureau.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PhotoModel PhotoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhotoModel = await _context.PhotoModel.FirstOrDefaultAsync(m => m.Id == id);

            if (PhotoModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PhotoModel = await _context.PhotoModel.FindAsync(id);

            if (PhotoModel != null)
            {
                _context.PhotoModel.Remove(PhotoModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
