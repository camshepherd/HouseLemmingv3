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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseLemmingv3.Pages.Manage.Requests
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public string SearchString { get; set; }
        public SelectList Status { get; set; }
        private UserManager<ApplicationUser> _UserManager { get; set; }

        public bool ShowEdit { get; set; }

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            _UserManager = _context.GetService<UserManager<ApplicationUser>>();
        }

        public IList<Request> Requests { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            Guid UserId = _UserManager.GetUserAsync(HttpContext.User).Result.Id;
            IList<string> Role = _UserManager.GetRolesAsync(_UserManager.GetUserAsync(HttpContext.User).Result).Result;
            var requests = from m in _context.Requests.Include(r => r.Advert).Include(r => r.Advert.ApplicationUser)
                select m;
            if (Role.Contains("Admin"))
            {
                requests = from m in _context.Requests.Include(r => r.Advert).Include(r => r.Advert.ApplicationUser).Where(u => u.Advert.ApplicationUserId == UserId)
                    select m;
                ShowEdit = true;
            }
            else 
            {
                ShowEdit = false;
            }
            if (!String.IsNullOrEmpty(searchString) && searchString.All(Char.IsDigit))
            {
                int result = Int32.Parse(searchString);
                requests = requests.Where(s => s.Approval == result);
            }
            else
            {
                requests = requests.Where(s => s.Approval == 1);
            }

            Requests = await requests.ToListAsync();
            SearchString = searchString;
        }
    }
}
