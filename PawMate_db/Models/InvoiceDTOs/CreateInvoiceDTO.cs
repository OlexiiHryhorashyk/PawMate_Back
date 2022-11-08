namespace Models.InvoiceDTOs
{
    public class CreateInvoiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public int UserId { get; set; }
    }
}
