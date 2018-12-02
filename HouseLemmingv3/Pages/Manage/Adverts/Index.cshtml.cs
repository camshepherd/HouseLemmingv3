using System;
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

namespace HouseLemmingv3.Pages.Manage.Adverts
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            _UserManager = _context.GetService<UserManager<ApplicationUser>>();
        }

        private readonly UserManager<ApplicationUser> _UserManager;
        public IList<Advert> Advert { get;set; }
        [BindProperty]
        public bool ShowCreate { get; set; }

        public bool ShowEdit { get; set; }

        public async Task OnGetAsync()
        {
            Guid UserId = _UserManager.GetUserAsync(HttpContext.User).Result.Id;
            IList<string> Role = _UserManager.GetRolesAsync(_UserManager.GetUserAsync(HttpContext.User).Result).Result;
            if (Role.Contains("Landlord"))
            {
                ShowCreate = true;
                ShowEdit = true;
                Advert = await _context.Adverts
                    .Include(a => a.ApplicationUser).Where(u => u.ApplicationUserId == UserId).ToListAsync();
            }
            else
            {
                ShowEdit = false;
                ShowCreate = false;
                Advert = await _context.Adverts.Include(a => a.ApplicationUser).ToListAsync();
            }
        }
    }
}
