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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PhotoModel> PhotoModel { get;set; }

        public async Task OnGetAsync()
        {
            PhotoModel = await _context.PhotoModel.ToListAsync();
        }
    }
}
