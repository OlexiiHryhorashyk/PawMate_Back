using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMate.Domain.RepositoryInterfaces
{
    public interface IRepositoryManager
    {
        ILikedRepository LikedRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IUserRepository UserRepository { get; }
        Task SaveAsync();
    }
}
