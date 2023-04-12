using Gamification.Entities;

namespace Gamification.Repositories
{
    public interface ITokenRepository
    {
        Task<Token> AddTokenAsync(Token t);
        Task<Token> GetTokenById(Guid id);
        Task<UserToken> AddUserTokenAsync(UserToken t);
        Task<List<Token>> GetTokensByUserIdAsync(Guid userId);
    }
}
