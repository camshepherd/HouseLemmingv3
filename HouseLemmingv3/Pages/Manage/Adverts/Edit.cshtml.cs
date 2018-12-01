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

namespace HouseLemmingv3.Pages.Manage
{
    public class EditModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public EditModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Advert Advert { get; set; }

        [BindProperty]
        public bool GoLive { get; set; }

        private Request Request { get; set; }

        private bool MakeRequest = false;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advert = await _context.Adverts
                .Include(a => a.ApplicationUser).FirstOrDefaultAsync(m => m.AdvertId == id);
            if (Advert.Status == 1)
            {
                GoLive = true;
            }
            else
            {
                GoLive = false;
            }
            
            if (Advert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!GoLive)
            {
                Advert.Status = 0;
            }
            else
            {
                if (_context.Adverts.Find(Advert.AdvertId).Status == 1)
                {
                    Advert.Status = 1;
                }
                else
                {
                    Advert.Status = 1;
                    if (_context.Adverts.Find(Advert.AdvertId).Requests.Where(u => u.Approval == 1).Any())
                    {
                        _context.Requests.RemoveRange(_context.Adverts.Find(Advert.AdvertId).Requests
                            .Where(u => u.Approval == 1));
                    }

                    MakeRequest = true;
                }
            }

            
            _context.Attach(Advert).State = EntityState.Modified;
            //_context.Attach(Advert).State = EntityState.Unchanged;

            if (MakeRequest)
            {
                Request = new Request();
                Request.AdvertId = Advert.AdvertId;
                Request.Approval = 1;
                Request.DateCreation = DateTime.Now;

                _context.Requests.Add(Request);
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertExists(Advert.AdvertId))
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

        private bool AdvertExists(Guid id)
        {
            return _context.Adverts.Any(e => e.AdvertId == id);
        }
    }
}
