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
using Microsoft.IdentityModel.Tokens;

namespace HouseLemmingv3.Pages.Manage.Requests
{
    public class EditModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public EditModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }

        public Guid AdvertId;
        public string AdvertName;
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.Requests
                .Include(r => r.Advert).FirstOrDefaultAsync(m => m.RequestId == id);
            ViewData["AdvertId"] = Request.AdvertId;
            ViewData["AdvertName"] = Request.Advert.AddrLine1;
            AdvertName = Request.Advert.AddrLine1;
            if (Request == null)
            {
                return NotFound();
            }
            //ViewData["AdvertId"] = new SelectList(_context.Adverts, "AdvertId", "AddrCity");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Request.DateResponse = DateTime.Now;
            //_context.Attach(Request).State = EntityState.Unchanged;
            _context.Attach(Request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.RequestId))
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

        private bool RequestExists(Guid id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
