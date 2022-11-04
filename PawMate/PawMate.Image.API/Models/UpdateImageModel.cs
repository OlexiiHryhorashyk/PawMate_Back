using System.ComponentModel.DataAnnotations;

namespace PawMate.Image.API.Models
{
    public class UpdateImageModel
    {
        [Required]
        public string OldImageUrl { get; set; }

        [Required]
        public IFormFile NewImage { get; set; }
    }
}
