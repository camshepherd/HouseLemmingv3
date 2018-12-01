using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;

namespace HouseLemmingv3.Pages.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public DeleteModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Advert Advert { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advert = await _context.Adverts
                .Include(a => a.ApplicationUser).FirstOrDefaultAsync(m => m.AdvertId == id);

            if (Advert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advert = await _context.Adverts.FindAsync(id);

            if (Advert != null)
            {
                _context.Adverts.Remove(Advert);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
