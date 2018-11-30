using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;

namespace HouseLemmingv3.Pages.Manage
{
    public class EditModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public EditModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Advert Advert { get; set; }

        public Guid applicationUserId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advert = await _context.Adverts
                .Include(a => a.ApplicationUser).FirstOrDefaultAsync(m => m.AdvertId == id);
            applicationUserId = Advert.ApplicationUserId;

            if (Advert == null)
            {
                return NotFound();
            }
           ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Advert).State = EntityState.Modified;
            _context.Attach(Advert).State = EntityState.Unchanged;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertExists(Advert.AdvertId))
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

        private bool AdvertExists(Guid id)
        {
            return _context.Adverts.Any(e => e.AdvertId == id);
        }
    }
}
