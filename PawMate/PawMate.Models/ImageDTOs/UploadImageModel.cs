using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PawMate.Models.ImageDTOs
{
    public class UploadImageModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
