using PawMate.Domain.Entities;
using PawMate.Domain.RepositoryInterfaces;
using Persistence.Respositories;

namespace Persistence.Repositories
{
    public class LikedRepository : GenericRepository<Liked>, ILikedRepository
    {
        public LikedRepository(ApplicationContext applicationContext)
            :base(applicationContext)
        { 
        }
    }
}
