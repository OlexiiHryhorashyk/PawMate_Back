using AutoMapper;
using PawMate.Domain.Entities;
using PawMate.Domain.RepositoryInterfaces;
using PawMate.Models.UserDTOs;
using Services.Interfaces;

namespace PawMate.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _user;
        private IMapper _mapper;

        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _user = repository.UserRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> AddUser(CreateUserDTO user)
        {
            var addUser = _mapper.Map<User>(user);
            await _user.CreateUserAsync(addUser);
            return _mapper.Map<UserDTO>(addUser);
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _user.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(users.ToList());
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            return _mapper.Map<UserDTO>(await _user.GetByIdAsync(userId));
        }

        public async Task<UserDTO> UpdateUserAsync(UpdateUserDTO user)
        {
            var updateUser = _mapper.Map<User>(user);
            await _user.UpdateUserAsync(updateUser);
            return _mapper.Map<UserDTO>(updateUser);
        }

        public async Task<int> DeleteUserById(int userId)
        {
            await _user.DeleteUserAsync(userId);
            return userId;
        }
    }
}
