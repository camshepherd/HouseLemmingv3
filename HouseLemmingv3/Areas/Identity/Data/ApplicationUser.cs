using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace HouseLemmingv3.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public List<Advert> Adverts { get; set; }
    }
}
