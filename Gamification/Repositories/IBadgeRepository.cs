using Gamification.Entities;

namespace Gamification.Repositories
{
    public interface IBadgeRepository
    {
        Task<Badge> AddBadgeAsync(Badge t);
        Task<Badge> FindById(Guid id);
        Task<UserBadge> AddUserBadgeAsync(UserBadge t);
     Task<List<Badge>> GetBadgesByUserIdAsync(Guid userId);
    }
}
