using Models.InvoiceDTOs;

namespace Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDTO>> GetAllInvoices();
        Task<InvoiceDTO> GetInvoiceById(int Id);
        Task<List<InvoiceDTO>> GetInvoicesByUserId(int userId);
        Task<InvoiceDTO> AddInvoice(CreateInvoiceDTO invoice);
        Task<InvoiceDTO> UpdateInvoice(UpdateInvoiceDTO invoice);
        Task<int> DeleteInvoice(int id);
        Task<InvoiceDTO> AddLike(int invoiceId, int userId);
    }
}
