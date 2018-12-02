using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HouseLemmingv3.Data;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using HouseLemmingv3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseLemmingv3.Utilities
{
    public class ImageHelpers
    {
        private ApplicationDbContext _context;
        public ImageHelpers(ApplicationDbContext context)
        {
            _context = context;
        }

        public static async Task<byte[]> ProcessFormFile(IFormFile imageFile,
            ModelStateDictionary modelState)
        {
            var fieldDisplayName = string.Empty;

            // Use reflection to obtain the display name for the model 
            // property associated with this IFormFile. If a display
            // name isn't found, error messages simply won't show
            // a display name.
            MemberInfo property =
                typeof(FileUpload).GetProperty(
                    imageFile.Name.Substring(imageFile.Name.IndexOf(".") + 1));

            if (property != null)
            {
                var displayAttribute =
                    property.GetCustomAttribute(typeof(DisplayAttribute))
                        as DisplayAttribute;

                if (displayAttribute != null)
                {
                    fieldDisplayName = $"{displayAttribute.Name} ";
                }
            }

            // Use Path.GetFileName to obtain the file name, which will
            // strip any path information passed as part of the
            // FileName property. HtmlEncode the result in case it must 
            // be returned in an error message.
            var fileName = WebUtility.HtmlEncode(
                Path.GetFileName(imageFile.FileName));

            if (imageFile.ContentType.ToLower() != "image/jpeg" && imageFile.ContentType.ToLower() != "image/jpg")
            {
                modelState.AddModelError(imageFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) must be a text file.");
            }

            // Check the file length and don't bother attempting to
            // read it if the file contains no content. This check
            // doesn't catch files that only have a BOM as their
            // content, so a content length check is made later after 
            // reading the file's content to catch a file that only
            // contains a BOM.
            if (imageFile.Length == 0)
            {
                modelState.AddModelError(imageFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) is empty.");
            }
            else if (imageFile.Length > 4194304)
            {
                modelState.AddModelError(imageFile.Name,
                    $"The {fieldDisplayName}file ({fileName}) exceeds 4 MB.");
            }
            else
            {
                try
                {
                    byte[] fileContents;

                    // The StreamReader is created to read files that are UTF-8 encoded. 
                    // If uploads require some other encoding, provide the encoding in the 
                    // using statement. To change to 32-bit encoding, change 
                    // new UTF8Encoding(...) to new UTF32Encoding().
                    using (var fs1 = imageFile.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        fileContents = ms1.ToArray();
                    }
                    

                    // Check the content length in case the file's only
                    // content was a BOM and the content is actually
                    // empty after removing the BOM.

                        return fileContents;

                    }
                catch (Exception ex)
                {
                    modelState.AddModelError(imageFile.Name,
                        $"The {fieldDisplayName}file ({fileName}) upload failed. " +
                        $"Please contact the Help Desk for support. Error: {ex.Message}");
                    // Log the exception
                }
            }

            return null;
        }

        [HttpGet]
        public FileStreamResult ViewImage(int Id)
        {

            byte[] ByteArray = _context.Images.FirstOrDefaultAsync(m => m.ImageId == Id).Result.ImageBytes;

            MemoryStream ms = new MemoryStream((byte[]) ByteArray,0,ByteArray.Length);
            //string thing = ms.ToString();
            
            return new FileStreamResult(ms, "image/jpeg");
        }
        
    }
}