using Gamification.Entities;

namespace Gamification.Service
{
    public interface IRewardService
    {
        Task<UserRewards> BuyItemAsync(Guid userId, Guid itemId);
        Task<Rewards> AddReward(Rewards r);
    }
}
