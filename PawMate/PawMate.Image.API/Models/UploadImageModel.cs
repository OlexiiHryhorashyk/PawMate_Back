using System.ComponentModel.DataAnnotations;

namespace PawMate.Image.API.Models
{
    public class UploadImageModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
