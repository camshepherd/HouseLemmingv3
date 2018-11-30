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

namespace HouseLemmingv3.Pages.Images2
{
    public class EditModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public EditModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Image Image { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Image = await _context.Images
                .Include(i => i.Advert).FirstOrDefaultAsync(m => m.ImageId == id);

            if (Image == null)
            {
                return NotFound();
            }
           ViewData["AdvertId"] = new SelectList(_context.Adverts, "AdvertId", "AddrCity");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(Image.ImageId))
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

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
