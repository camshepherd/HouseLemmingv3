﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using HouseLemmingv3.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseLemmingv3.Pages.Manage.Adverts

{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public DetailsModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            _UserManager = _context.GetService<UserManager<ApplicationUser>>();
        }

        public UserManager<ApplicationUser> _UserManager;
        public Advert Advert { get; set; }
        public bool ShowEdit { get; set; }
        public bool ShowStates { get; set; }


        [BindProperty]
        public ImageHelpers ImageHelpers { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            try
            {
                ImageHelpers = new ImageHelpers(_context);
                Guid UserId = _UserManager.GetUserAsync(HttpContext.User).Result.Id;
                IList<string> Role = _UserManager.GetRolesAsync(_UserManager.GetUserAsync(HttpContext.User).Result)
                    .Result;
                if (id == null)
                {
                    return NotFound();
                }

                if (Role.Contains("Landlord") || Role.Contains("Admin"))
                {
                    ShowEdit = true;
                    ShowStates = true;
                }
                else
                {
                    ShowStates = false;
                    ShowEdit = false;
                }
            }
            catch (Exception e)
            {
                ShowStates = false;
                ShowEdit = false;
            }

            Advert = await _context.Adverts
                .Include(a => a.ApplicationUser).Include(t => t.Images).Include(c => c.Requests).FirstOrDefaultAsync(m => m.AdvertId == id);
                
            if (Advert == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
