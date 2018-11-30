using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;

namespace HouseLemmingv3.Pages.Images2
{
    public class CreateModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public CreateModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AdvertId"] = new SelectList(_context.Adverts, "AdvertId", "AddrCity");
            return Page();
        }

        [BindProperty]
        public Image Image { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Images.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}