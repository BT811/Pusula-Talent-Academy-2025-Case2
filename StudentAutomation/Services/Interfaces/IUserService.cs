using StudentAutomation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAutomation.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> RegisterUser(User model);
    }
}
