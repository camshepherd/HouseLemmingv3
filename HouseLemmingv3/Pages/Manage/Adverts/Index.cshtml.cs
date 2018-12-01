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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseLemmingv3.Pages.Manage
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            UserManager = _context.GetService<UserManager<ApplicationUser>>();
        }

        private UserManager<ApplicationUser> UserManager;
        private RoleManager<IdentityRole> RoleManager;
        public IList<Advert> Advert { get;set; }
        

        public async Task OnGetAsync()
        {
            Guid UserId = UserManager.GetUserAsync(HttpContext.User).Result.Id;
            IList<string> Role = UserManager.GetRolesAsync(UserManager.GetUserAsync(HttpContext.User).Result).Result;
            if (Role.Contains("Landlord"))
            {
                Advert = await _context.Adverts
                    .Include(a => a.ApplicationUser).Where(u => u.ApplicationUserId == UserId).ToListAsync();
            }
            else
            {
                Advert = await _context.Adverts.Include(a => a.ApplicationUser).ToListAsync();
            }
        }
    }
}
