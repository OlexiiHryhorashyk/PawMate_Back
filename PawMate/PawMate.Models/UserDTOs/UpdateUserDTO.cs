using Microsoft.AspNetCore.Http;

namespace PawMate.Models.UserDTOs
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
    }
}
