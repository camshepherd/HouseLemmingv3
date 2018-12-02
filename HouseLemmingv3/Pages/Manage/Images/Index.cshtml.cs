﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Areas.Identity.Data;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Models;
using HouseLemmingv3.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseLemmingv3.Pages.Images
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            ImageHelpers = new ImageHelpers(context);
            UserManager = _context.GetService<UserManager<ApplicationUser>>();
        }
        private UserManager<ApplicationUser> UserManager;
        public ImageHelpers ImageHelpers;

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        [BindProperty]
        public Guid AdvertGuid { get; set; }

        [BindProperty]public Image Image { get; set; }

        public IList<Image> Images { get; private set; }


        public async Task OnGetAsync()
        {
            IList<string> Role = UserManager.GetRolesAsync(UserManager.GetUserAsync(HttpContext.User).Result).Result;
            if (Role.Contains("Admin"))
            {
                ViewData["AdvertId"] =
                    new SelectList(
                        _context.Adverts,
                        "AdvertId",
                        "AddrLine1");
                Images = await _context.Images
                    .Include(e => e.Advert).ToListAsync();
            }
            else if (Role.Contains("Landlord"))
            {
                ViewData["AdvertId"] =
                    new SelectList(
                        _context.Adverts.Where(u =>
                            u.ApplicationUserId == UserManager.GetUserAsync(HttpContext.User).Result.Id), "AdvertId",
                        "AddrLine1");
                Images = await _context.Images
                    .Where(k => k.Advert.ApplicationUserId == UserManager.GetUserAsync(HttpContext.User).Result.Id)
                    .Include(e => e.Advert).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Perform an initial check to catch FileUpload class
            // attribute violations.
            if (!ModelState.IsValid)
            {
                Images = await _context.Images.Include(a => a.Advert).ToListAsync();
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