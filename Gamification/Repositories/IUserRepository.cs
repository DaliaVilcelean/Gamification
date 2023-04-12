using Gamification.DTOs;
using Gamification.Entities;
using System.Threading.Tasks;

namespace Gamification.Repositories
{
    public interface IUserRepository:IBaseRepository<User>
    {
        Task<User> Create(User t);
        Task<User> Login(string username, string password);
        Task<List<User>> GetUsersByEmailAsync(string email);
        Task UpdateUserNrTokensAsync(User t);
    }
}
