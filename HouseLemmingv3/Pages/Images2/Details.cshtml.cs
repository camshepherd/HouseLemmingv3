using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;

namespace HouseLemmingv3.Pages.Images2
{
    public class DetailsModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public DetailsModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
