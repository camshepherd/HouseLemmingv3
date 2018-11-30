using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HouseLemmingv3.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "File Name")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
