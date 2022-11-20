using PawMate.Domain.RepositoryInterfaces;
using Persistence.Respositories;

namespace Persistence.Repositories
{
    public class RepositoryManager: IRepositoryManager
    {
        private readonly ApplicationContext _context;

        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context);
        public IInvoiceRepository InvoiceRepository => new InvoiceRepository(_context);
        public ILikedRepository LikedRepository => new LikedRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
