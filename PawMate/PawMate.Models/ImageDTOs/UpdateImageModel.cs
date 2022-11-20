using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PawMate.Models.ImageDTOs
{
    public class UpdateImageModel
    {
        [Required]
        public string OldImageUrl { get; set; }

        [Required]
        public IFormFile NewImage { get; set; }
    }
}
