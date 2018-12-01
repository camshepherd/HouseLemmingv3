﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HouseLemmingv3.Pages.Manage.Requests
{
    public class IndexModel : PageModel
    {
        private readonly HouseLemmingv3.Data.ApplicationDbContext _context;

        public string SearchString { get; set; }
        public SelectList Status { get; set; }

        public IndexModel(HouseLemmingv3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Request> Requests { get;set; }

        public async Task OnGetAsync(string searchString)
        {
            var requests = from m in _context.Requests.Include(r => r.Advert)
                select m;

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
