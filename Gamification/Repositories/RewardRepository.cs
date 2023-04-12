using Gamification.Context;
using Gamification.Entities;
using System.Data.Entity;

namespace Gamification.Repositories
{
    public class RewardRepository : IRewardRepository
    {

        private readonly GameDbContext _dbContext;

        public RewardRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Rewards> FindById(Guid id)
        {
            return await _dbContext.Rewards.FindAsync(id);
        }

        public async Task<List<Rewards>> FindAll()
        {
            return await _dbContext.Rewards.ToListAsync();
        }

        public async Task<Rewards> Create(Rewards reward)
        {
            var entity = new Rewards
            {
                Id = Guid.NewGuid(),
                Description = reward.Description,
                Name = reward.Name,
                PointsNeeded = reward.PointsNeeded,
            };

        await _dbContext.Rewards.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Rewards> Update(Rewards reward)
        {
            var entity = new Rewards
            {
                Id = reward.Id,
                Description = reward.Description,
                Name = reward.Name,
                PointsNeeded = reward.PointsNeeded,
            };
            _dbContext.Rewards.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Rewards t)
        {
            var reward = await FindById(t.Id);
            _dbContext.Rewards.Remove(reward);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserRewards> AddUserRewardAsync(UserRewards userReward)
        {
            await _dbContext.UserRewards.AddAsync(userReward);
            await _dbContext.SaveChangesAsync();

            return userReward;
        }

        
    }
}
