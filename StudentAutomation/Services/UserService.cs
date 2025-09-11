using StudentAutomation.Models;
using StudentAutomation.Repositories.Interfaces;
using StudentAutomation.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAutomation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }


        public async Task<bool> RegisterUser(User model)
        {
            if (await _userRepository.EmailExists(model.Email))
                return false;

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                Role = model.Role
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<User> GetAllStudentsAsync()
        {
            return await _userRepository.GetByEmailAsync("Student");
        }
    }
}
