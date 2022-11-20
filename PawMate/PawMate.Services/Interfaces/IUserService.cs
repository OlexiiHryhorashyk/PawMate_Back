using PawMate.Models.UserDTOs;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> AddUser(CreateUserDTO user);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserById(int userId);
        Task<UserDTO> UpdateUserAsync(UpdateUserDTO user);
        Task<int> DeleteUserById(int userId);

    }
}
