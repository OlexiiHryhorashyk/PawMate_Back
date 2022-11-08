using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Respositories;
using Domain.RepositoryInterfaces;


namespace Persistence.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
            : base(context)
    { }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await FindAll()
            .ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await FindByCondition(User => User.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<User> GetByNameAsync(string firstName, string lastName)
    {
        return await FindByCondition(User => User.Name == firstName
            && User.Surname == lastName)
            .FirstOrDefaultAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        return await CreateAsync(user);
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        return await DeleteAsync(
            FindByCondition(User => User.Id == id)
            .FirstOrDefault());
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        return await UpdateAsync(user);
    }
}
}
