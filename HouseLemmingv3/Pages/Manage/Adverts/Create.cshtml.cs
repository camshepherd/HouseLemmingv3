using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HouseLemmingv3.Pages.Manage.Adverts
{
    public class CreateModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;
        private UserManager<ApplicationUser> UserManager;
        public CreateModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
            UserManager = context.GetService<UserManager<ApplicationUser>>();
        }

        public IActionResult OnGet()
        {
        ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Advert Advert { get; set; }
        
        [BindProperty]
        public bool GoLive { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Advert.ApplicationUserId = UserManager.GetUserAsync(HttpContext.User).Result.Id;
            if (!GoLive)
            {
                Advert.Status = 0;
                _context.Adverts.Add(Advert);
            }
            else
            {
                Advert.Status = 1;
                Request request = new Request();
                request.AdvertId = Advert.AdvertId;
                request.Approval = 1;
                request.DateCreation = DateTime.Now;
                _context.Adverts.Add(Advert);
                _context.Requests.Add(request);
            }

            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}