using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByNameAsync(string firstName, string lastName);
        Task<User> CreateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
        Task<User> UpdateUserAsync(User user);
    }
}
