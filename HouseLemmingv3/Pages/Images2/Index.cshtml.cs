using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;

namespace HouseLemmingv3.Pages.Images2
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Image> Image { get;set; }

        public async Task OnGetAsync()
        {
            Image = await _context.Images
                .Include(i => i.Advert).ToListAsync();
        }
    }
}
