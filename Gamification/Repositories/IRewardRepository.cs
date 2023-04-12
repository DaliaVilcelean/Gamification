using Gamification.Entities;

namespace Gamification.Repositories
{
    public interface IRewardRepository:IBaseRepository<Rewards>
    {

        Task<UserRewards> AddUserRewardAsync(UserRewards userReward);
        Task<Rewards> Create(Rewards rewards);

    }
}
