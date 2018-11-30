using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Models;
using HouseLemmingv3.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseLemmingv3.Pages.Images
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        [BindProperty]
        public Guid AdvertGuid { get; set; }

        [BindProperty]public Image Image { get; set; }

        public IList<Image> Images { get; private set; }


        public async Task OnGetAsync()
        {
            ViewData["AdvertId"] = new SelectList(_context.Adverts, "AdvertId", "AddrLine1");
            Images = await _context.Images.AsNoTracking().ToListAsync();
        }
        /*
        public IActionResult OnGet()
        {
            ViewData["AdvertId"] = new SelectList(_context.Adverts, "AdvertId", "AddrLine1");
            return Page();
        }
        */
        public async Task<IActionResult> OnPostAsync()
        {
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                Images = await _context.Images.AsNoTracking().ToListAsync();
                return Page();
            }

            byte[] imageData =
                await ImageHelpers.ProcessFormFile(FileUpload.ImageFile, ModelState);


            // Perform a second check to catch ProcessFormFile method
            // violations.
            if (!ModelState.IsValid)
            {
                Images = await _context.Images.AsNoTracking().ToListAsync();
                return Page();
            }

            Image image = new Image()
            {
                ImageBytes = imageData,
                FileName = FileUpload.Title,
                AdvertId = AdvertGuid
            };

            _context.Images.AddAsync(image).Wait();
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}