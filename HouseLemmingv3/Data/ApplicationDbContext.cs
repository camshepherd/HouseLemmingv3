using System;
using System.Collections.Generic;
using System.Text;
using HouseLemmingv3.Areas.Identity.Data.WebApp1.Areas.Identity.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseLemmingv3.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Request> Requests { get; set; }

       /* protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Adverts)
                .WithOne(k => k.ApplicationUser);

            modelBuilder.Entity<Advert>()
                .HasMany(u => u.Requests)
                .WithOne(t => t.Advert);
        }*/



    }
}
