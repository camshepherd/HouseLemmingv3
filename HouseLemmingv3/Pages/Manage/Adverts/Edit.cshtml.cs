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

namespace HouseLemmingv3.Pages.Manage.Adverts

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

        [BindProperty]
        public int InitialStatus { get; set; }

        public bool MakeRequest = false;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            MakeRequest = false;
            if (id == null)
            {
                return NotFound();
            }

            
            Advert = await _context.Adverts.FirstOrDefaultAsync(m => m.AdvertId == id);
            InitialStatus = Advert.Status;
            if (Advert.Status == 0)
            {
                GoLive = false;
            }
            else
            {
                GoLive = true;
            }

            if (Advert == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            MakeRequest = false;
            if (GoLive)
            {
                Advert.Status = 1;
            }
            else
            {
                Advert.Status = 0;
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Advert.Status == 1)
            {
                if (InitialStatus != 1)
                {
                    MakeRequest = true;
                }
            }

            //Advert thing = _context.Adverts.Where(A => A.AdvertId == Advert.AdvertId);
            //_context.Remove(_context.Adverts.Where(A => A.AdvertId == Advert.AdvertId).GetEnumerator().Current);
            _context.Attach(Advert).State = EntityState.Modified;
            //_context.Attach(Advert).State = EntityState.Unchanged;

            if (MakeRequest)
            {
                Request request = new Request()
                {
                    AdvertId = Advert.AdvertId,
                    Approval = 1,
                    DateCreation = DateTime.Now
                };

                _context.Requests.Add(request);
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
