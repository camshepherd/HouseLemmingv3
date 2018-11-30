using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLemmingv3.Models
{
    public class Request
    {
        [Key]
        [Required]
        public Guid RequestId { get; set; }

        [Required] public Guid AdvertId { get; set; }
        public Advert Advert { get; set; }

        [MaxLength(140)] public string Feedback { get; set; }

        [Required]
        [Range(0, 2)]
        //Let 0=declined,1=unaddressed,2=accepted
        [DefaultValue(1)]
        public int Approval { get; set; }

        [Required]
        [DataType(DataType.Date)] public DateTime DateCreation { get; set; }

        [DataType(DataType.Date)] public DateTime DateResponse { get; set; }
    }
}
