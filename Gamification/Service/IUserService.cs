using Gamification.DTOs;
using Gamification.Entities;

namespace Gamification.Service
{
    public interface IUserService
    {
        Task<UserDTOs> SignupAsync(User t);
        Task<UserDTO> FindById(Guid id);
        Task<LoginResponseDTO> LoginAsync(string email, string password);
        Task<UserDTO>  GetUserDetailsAsync(Guid userId);
        Task<List<UserDTO>> GetAllUsersWithBadgesTokensQuestsAsync();
    }
}
