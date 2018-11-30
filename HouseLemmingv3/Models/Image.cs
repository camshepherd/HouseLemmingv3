using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLemmingv3.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public string FileName { get; set; }

        [Required] public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        public string FilePath { get; set; }
    }
}
