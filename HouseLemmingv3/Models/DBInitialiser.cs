using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseLemmingv3.Areas.Identity.Data.WebApp1.Areas.Identity.Data;
using HouseLemmingv3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HouseLemmingv3.Models
{
    public class DBInitialiser
    {
        public static async Task InitialiseAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Student", "Supervisor", "Landlord" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                }
            }
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
                            "Administrator").Wait();
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
                        (user, "fjfnHJf8d(").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user,
                            "Administrator").Wait();
                    }
                }

                
                if (!context.Adverts.Any())
                {
                    context.Adverts.AddRange(
                        new Advert
                        {
                            OwnerId = context.Users
                                .SingleOrDefault(user => user.UserName == "liane@peppermintelephant.com").Id,
                            DescShort = "Small House Really Nice ",
                            DescLong =
                                "Small House Really Nice, \n PLZ BUY!!, cockroaches everywhere, nobody wants to buy :( ",
                            NumToilets = 2,
                            NumRooms = 7,
                            PriceMonthly = 230.40f,
                            Deposit = 500,
                            StartDate = new DateTime(2018, 12, 24, 0, 0, 0),
                            EndDate = new DateTime(2019, 02, 01, 0, 0, 0),
                            Status = 1,
                            ContactNumber = "07536123456",
                            ContactEmail = "enquiries@abc.me",
                            AddrLine1 = "230 Burgess Road",
                            AddrLine2 = "",
                            AddrCity = "Southampton",
                            AddrCounty = "Hampshire",
                            AddrPostCode = "SO437JE"
                        },
                        new Advert
                        {
                            OwnerId = context.Users.SingleOrDefault(user => user.UserName == "stu@aol.com").Id,
                            DescShort = "Small House Really Nice,REALLY ",
                            DescLong =
                                "Small House Really Nice, \n PLZ BUY!!, cockroaches everywhere, nobody wants to buy :( ",
                            NumToilets = 2,
                            NumRooms = 7,
                            PriceMonthly = 230.40f,
                            Deposit = 500,
                            StartDate = new DateTime(2018, 12, 24, 0, 0, 0),
                            EndDate = new DateTime(2019, 02, 01, 0, 0, 0),
                            Status = 0,
                            ContactNumber = "07536123456",
                            ContactEmail = "enquiries@abc.me",
                            AddrLine1 = "230b Burgess Road",
                            AddrLine2 = "",
                            AddrCity = "Southampton",
                            AddrCounty = "Hampshire",
                            AddrPostCode = "SO437JE"
                        },
                        new Advert
                        {
                            OwnerId = context.Users
                                .SingleOrDefault(user => user.UserName == "broflovski_attorney@aol.com").Id,
                            DescShort = "averagehouse nothing unusual ",
                            DescLong = "its so normal even normal doesnt look normal",
                            NumToilets = 1,
                            NumRooms = 5,
                            PriceMonthly = 500.10f,
                            Deposit = 500,
                            StartDate = new DateTime(2018, 12, 12, 0, 0, 0),
                            EndDate = new DateTime(2019, 11, 12, 0, 0, 0),
                            Status = 1,
                            ContactNumber = "07982776542",
                            ContactEmail = "simple@example.com",
                            AddrLine1 = "24 Normal Avenue",
                            AddrLine2 = "",
                            AddrCity = "Norlands",
                            AddrCounty = "Hampshire",
                            AddrPostCode = "SO163LQ"
                        },
                        new Advert
                        {
                            OwnerId = context.Users
                                .SingleOrDefault(user => user.UserName == "broflovski_attorney@aol.com").Id,
                            DescShort = "Perfect House, cheap price ",
                            DescLong = "i want no money, so just live here, and give me your soul ",
                            NumToilets = 3,
                            NumRooms = 6,
                            PriceMonthly = 666.66f,
                            Deposit = 666,
                            StartDate = new DateTime(2018, 12, 06, 0, 0, 0),
                            EndDate = new DateTime(2019, 09, 09, 0, 0, 0),
                            Status = 0,
                            ContactNumber = "01234569781",
                            ContactEmail = "deVille@soul.come",
                            AddrLine1 = "66 Route",
                            AddrLine2 = "",
                            AddrCity = "Hell",
                            AddrCounty = "Hampshire",
                            AddrPostCode = "PO420JJ"
                        },
                        new Advert
                        {
                            OwnerId = context.Users
                                .SingleOrDefault(user => user.UserName == "liane@peppermintelephant.com").Id,
                            DescShort = "big Hous ",
                            DescLong =
                                "Big house, what else do you want???? Prfect, big bedrooms, 3 livingrooms, 20 people live here ",
                            NumToilets = 1,
                            NumRooms = 20,
                            PriceMonthly = 636.24f,
                            Deposit = 1000,
                            StartDate = new DateTime(2019, 04, 24, 0, 0, 0),
                            EndDate = new DateTime(2020, 08, 07, 0, 0, 0),
                            Status = 1,
                            ContactNumber = "07947779700",
                            ContactEmail = "onetoilet@idc.fr",
                            AddrLine1 = "some body just told me",
                            AddrLine2 = "",
                            AddrCity = "The",
                            AddrCounty = "Endshire",
                            AddrPostCode = "SO236GQ"
                        },
                        new Advert
                        {
                            OwnerId = context.Users.SingleOrDefault(user => user.UserName == "randy@tegridy.com").Id,
                            DescShort = "bighouse, but tight",
                            DescLong =
                                "its so big you coul dget lost in it!! rEally, come and see, you will like it so much you will never leave! ",
                            NumToilets = 44,
                            NumRooms = 404,
                            PriceMonthly = 404.00f,
                            Deposit = 404,
                            StartDate = new DateTime(2019, 04, 04, 0, 0, 0),
                            EndDate = new DateTime(2020, 04, 04, 0, 0, 0),
                            Status = 1,
                            ContactNumber = "07404404404",
                            ContactEmail = "get@lost.hs",
                            AddrLine1 = "Nowhere",
                            AddrLine2 = "",
                            AddrCity = "Town",
                            AddrCounty = "Paradoxshire",
                            AddrPostCode = "SO404HQ"
                        },
                        new Advert
                        {
                            OwnerId = context.Users
                                .SingleOrDefault(user => user.UserName == "broflovski_attorney@aol.com").Id,
                            DescShort = "sue house",
                            DescLong =
                                "Nice house out in teh country , simple house for simple clean honest living folk",
                            NumToilets = 2,
                            NumRooms = 5,
                            PriceMonthly = 40.00f,
                            Deposit = 0,
                            StartDate = new DateTime(2019, 01, 1, 0, 0, 0),
                            EndDate = new DateTime(2020, 01, 01, 0, 0, 0),
                            Status = 1,
                            ContactNumber = "07764847444",
                            ContactEmail = "randy@tegridy.com",
                            AddrLine1 = "1 Tegridy",
                            AddrLine2 = "Farmland",
                            AddrCity = "Grantham",
                            AddrCounty = "Lincolnshire",
                            AddrPostCode = "POJ7873"
                        }
                    );
                    context.SaveChanges();
                }


                if (!context.Requests.Any())
                {
                    context.Requests.AddRange(
                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "1 Tegridy")
                                .AdvertId,
                            Approval = 0,
                            DateCreation = new DateTime(2018, 11, 13, 22, 04, 01),
                            DateResponse = new DateTime(2018, 11, 21, 09, 38, 27),
                            Feedback = "Not enough information is posted"
                        },
                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "1 Tegridy")
                                .AdvertId,
                            Approval = 0,
                            DateCreation = new DateTime(2018, 11, 21, 23, 43, 01),
                            DateResponse = new DateTime(2018, 11, 23, 11, 15, 54),
                            Feedback = "No changes have beem actioned since the last attempt"
                        },
                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "1 Tegridy")
                                .AdvertId,
                            Approval = 2,
                            DateCreation = new DateTime(2018, 11, 24, 02, 52, 08),
                            DateResponse = new DateTime(2018, 11, 24, 15, 42, 19),
                            Feedback = "All looks to be good"
                        },

                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "Nowhere")
                                .AdvertId,
                            Approval = 0,
                            DateCreation = new DateTime(2018, 11, 21, 14, 49, 02),
                            DateResponse = new DateTime(2018, 11, 23, 18, 54, 02),
                            Feedback = "House does not seem realistic, I would recommend that changes are made to the listing to represent the real house, ",
                        },

                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "Nowhere")
                                .AdvertId,
                            Approval = 2,
                            DateCreation = new DateTime(2018, 11, 28, 09, 30, 28),
                            DateResponse = new DateTime(2018, 11, 28, 11, 59, 57),
                            Feedback = "Sent evidence was all good"
                        },

                        new Request
                        {
                            SubjectId = context.Adverts
                                .SingleOrDefault(thing => thing.AddrLine1 == "24 Normal Avenue").AdvertId,
                            Approval = 2,
                            DateCreation = new DateTime(2018, 11, 28, 15, 43, 26),
                            DateResponse = new DateTime(2018, 11, 29, 10, 43, 09),
                            Feedback = "All good"
                        },

                        new Request
                        {
                            SubjectId = context.Adverts.SingleOrDefault(thing => thing.AddrLine1 == "66 Route")
                                .AdvertId,
                            Approval = 1,
                            DateCreation = new DateTime(2018, 11, 18, 16, 31, 47),
                        },

                        new Request
                        {
                            SubjectId = context.Adverts
                                .SingleOrDefault(thing => thing.AddrLine1 == "some body just told me").AdvertId,
                            Approval = 2,
                            DateCreation = new DateTime(2018, 11, 16, 20, 14, 52),
                            DateResponse = new DateTime(2018, 11, 16, 20, 43, 59),
                        },

                        new Request
                        {
                            SubjectId = context.Adverts
                                .SingleOrDefault(thing => thing.AddrLine1 == "230 Burgess Road").AdvertId,
                            Approval = 2,
                            DateCreation = new DateTime(2018, 11, 15, 13, 19, 32),
                            DateResponse = new DateTime(2018, 11, 16, 13, 49, 37),
                            Feedback = "All looks good, welcome to HouseLemming!"
                        }
                    );
                    context.SaveChanges();
                }

            }
        }
    }
}
