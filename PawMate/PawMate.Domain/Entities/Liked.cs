

namespace PawMate.Domain.Entities
{
    public class Liked
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InvoiceId { get; set; }
        public virtual User User { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
