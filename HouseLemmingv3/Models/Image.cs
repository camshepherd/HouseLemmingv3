using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HouseLemmingv3.Models
{
    public class Image
    {
        [Required] public Guid ImageId { get; set; }
        [Required] public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        [Required] public string ImagePath { get; set; }
    }
}
