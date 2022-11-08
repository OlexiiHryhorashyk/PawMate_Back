using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IInvoiceRepository: IGenericRepository<Invoice> 
    {
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> GetByIdAsync(int Id);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        Task<Invoice> DeleteInvoiceAsync(int Id);
        Task<Invoice> UpdateInvoiceAsync(Invoice invoice);
    }
}
