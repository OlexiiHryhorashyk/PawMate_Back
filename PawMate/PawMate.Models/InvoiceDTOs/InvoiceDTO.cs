using PawMate.Models.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMate.Models.InvoiceDTOs
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserNumber { get; set; }
        public int Likes { get; set; }
    }
}
