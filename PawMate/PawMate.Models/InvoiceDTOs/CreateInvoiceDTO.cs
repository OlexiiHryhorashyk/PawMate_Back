using Microsoft.AspNetCore.Http;

namespace PawMate.Models.InvoiceDTOs
{
    public class CreateInvoiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public IFormFile PhotoData { get; set; }
        public int UserId { get; set; }
    }
}
