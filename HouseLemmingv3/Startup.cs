using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseLemmingv3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireLandlordRole", policy => policy.RequireRole("Landlord"));
                options.AddPolicy("RequireAdminOrLandlordRole",
                    policy => policy.RequireRole(new string[] {"Landlord", "Admin"}));
            });

            services.AddMvc(o =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddRazorPagesOptions(options =>
                    {
                        options.Conventions.AuthorizePage("/Manage/Requests/Edit", "RequireAdminRole");
                        options.Conventions.AuthorizePage("/Manage/Users/Index", "RequireAdminRole");
                        options.Conventions.AuthorizePage("/Manage/Users/Create", "RequireAdminRole");
                        options.Conventions.AuthorizePage("/Manage/Users/Edit", "RequireAdminRole");
                        options.Conventions.AuthorizePage("/Manage/Users/Delete", "RequireAdminRole");
                        options.Conventions.AuthorizePage("/Manage/Requests/Details", "RequireAdminOrLandlordRole");
                        options.Conventions.AuthorizePage("/Manage/Requests/Index", "RequireAdminOrLandlordRole");
                        options.Conventions.AuthorizePage("/Manage/Images/Index","RequireAdminOrLandlordRole");
                        options.Conventions.AuthorizePage("/Manage/Adverts/Create", "RequireLandlordRole");
                        options.Conventions.AuthorizePage("/Manage/Adverts/Edit", "RequireLandlordRole");
                        options.Conventions.AuthorizePage("/Manage/Adverts/Delete", "RequireLandlordRole");
                    })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //IdentityInitialiser.SeedData(userManager, roleManager);
            app.UseMvc();
        }
    }
}
