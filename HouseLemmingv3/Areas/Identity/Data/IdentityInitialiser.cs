using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLemmingv3.Areas.Identity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public static class IdentityInitialiser{


        public static void SeedData(UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole<Guid>> roleManager)
        {
            //SeedRoles(roleManager);
            SeedUsers(userManager);
        }


        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            //Landlords
            if (userManager.FindByEmailAsync("randy@tegridy.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "randy@tegridy.com";
                user.Email = "randy@tegridy.com";
                user.Firstname = "Randy";
                user.Lastname = "Marsh";

                IdentityResult result = userManager.CreateAsync
                    (user, "_9jfkfjIUU").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Landlord").Wait();
                }
            }
            if (userManager.FindByEmailAsync("broflovski_attorney@aol.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "broflovski_attorney@aol.com";
                user.Email = "broflovski_attorney@aol.com";
                user.Firstname = "Gerald";
                user.Lastname = "Broflovski";

                IdentityResult result = userManager.CreateAsync
                    (user, "_9j8NYv+UU").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Landlord").Wait();
                }
            }
            if (userManager.FindByEmailAsync("stu@aol.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "stu@aol.com";
                user.Email = "stu@aol.com";
                user.Firstname = "Stuart";
                user.Lastname = "McCormick";

                IdentityResult result = userManager.CreateAsync
                    (user, "98nfYYv+UU").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Landlord").Wait();
                }
            }
            if (userManager.FindByEmailAsync("liane@peppermintelephant.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "liane@peppermintelephant.com";
                user.Email = "liane@peppermintelephant.com";
                user.Firstname = "Liane";
                user.Lastname = "Cartman";

                IdentityResult result = userManager.CreateAsync
                    (user, "_97nfHU").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Landlord").Wait();
                }
            }


            // Admins
            if (userManager.FindByEmailAsync("darth.arbok@hotmail.co.uk").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "darth.arbok@hotmail.co.uk";
                user.Email = "darth.arbok@hotmail.co.uk";
                user.Firstname = "Cameron";
                user.Lastname = "Shepherd";

                IdentityResult result = userManager.CreateAsync
                    (user, "(&FNhf6f5djd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Admin").Wait();
                }
            }
            if (userManager.FindByEmailAsync("mat@fakesite.co.uk").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "mat@fakesite.co.uk";
                user.Email = "mat@fakesite.co.uk";
                user.Firstname = "Mateusz";
                user.Lastname = "Marton";

                IdentityResult result = userManager.CreateAsync
                    (user, "fjfnHJf8d£").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Admin").Wait();
                }
            }
        }

        /*
        public static void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Landlord").Result)
            {
                IdentityRole<Guid> role = new IdentityRole<Guid>();
                role.Name = "Landlord";
                IdentityResult<Guid> roleResult = roleManager.
                    CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Supervisor").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Supervisor";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }

        }*/
    }

}
