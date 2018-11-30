using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HouseLemmingv3.Areas.Identity.Data;



namespace HouseLemmingv3.Models
{
    public class Advert
    {
            [Key]
            [Required]
            public Guid AdvertId { get; set; }

            [Required] public Guid ApplicationUserId { get; set; }
            public ApplicationUser ApplicationUser { get; set; }

            public List<Request> Requests { get; set; }
            public List<Image> Images { get; set; }

            [Required]
            [MaxLength(140)]
            [MinLength(4)]
            public string DescShort { get; set; }

            [Required]
            [MaxLength(10000)]
            [MinLength(20)]
            public string DescLong { get; set; }


            [Required]
            [Range(0, int.MaxValue)]
            public int NumToilets { get; set; }


            [Range(1, int.MaxValue, ErrorMessage = "There has to be as least one room")]
            [Required]
            public int NumRooms { get; set; }

            [Required] [DataType(DataType.Currency)] public float PriceMonthly { get; set; }
            [Required] [DataType(DataType.Currency)] public float Deposit { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; }
            [Required] [DataType(DataType.Date)] public DateTime EndDate { get; set; }


            // 0 = closed (unconfirmed / declined approval / room already taken / removed by owner) and hidden, 1 = open (available to rent) and visible 
            [Required]
            [Range(0, 1)]
            [DefaultValue(0)]
            public int Status { get; set; }

            // Do not want regex on the phone number as many phone numbers do not conform to standard format
            [Required]
            [RegularExpression(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$")]
            public string ContactNumber { get; set; }

            //Pretty sure that regex on email should be fine
            [Required]
            [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                               + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                               + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                               + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$")]
            //[RegularExpression("^\w + ([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
            public string ContactEmail { get; set; }



            [Required]
            [MaxLength(100)]
            [MinLength(5)]
            public string AddrLine1 { get; set; }

            [MaxLength(100)]
            [MinLength(4)]
            public string AddrLine2 { get; set; }

            [Required]
            [MaxLength(60)]
            [MinLength(4)]
            public string AddrCity { get; set; }

            [MaxLength(30)]
            [MinLength(4)]
            public string AddrCounty { get; set; }

            [Required]
            [RegularExpression("^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$")]
            public string AddrPostCode { get; set; }
    }
}
