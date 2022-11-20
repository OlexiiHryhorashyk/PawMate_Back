using PawMate.Domain.Entities;
using PawMate.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Persistence.Respositories
{
    internal class InvoiceRepository: GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await FindAll()
                .ToListAsync();
        }

        public async Task<Invoice> GetByIdAsync(int Id)
        {
            return await FindByCondition(Invoice => Invoice.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            return await CreateAsync(invoice);
        }

        public async Task<Invoice> DeleteInvoiceAsync(int Id)
        {
            var data = await FindByCondition(invoice => invoice.Id == Id)
                .FirstOrDefaultAsync();
            return await DeleteAsync(data);
        }

        public async Task<Invoice> UpdateInvoiceAsync(Invoice invoice)
        {
            return await UpdateAsync(invoice);
        }
    }    
}
