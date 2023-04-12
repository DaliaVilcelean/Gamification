using Gamification.Context;
using Gamification.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Repositories
{
    public class QuestRepository:IQuestRepository
    {

        private readonly GameDbContext _dbContext;

        public QuestRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Quest>> GetQuestsByUserIdAsync(Guid userId)
        {
            return await _dbContext.UserQuests
                .Where(b => b.UserId == userId)
                .Select(ub => ub.Quest)
                .ToListAsync();
        }

        public async Task<Quest> ProposeQuestAsync(Quest t)
        {

            var entity = new Quest()
            {

                Id=Guid.NewGuid(),
                Badge=t.Badge,
                BadgeId=t.BadgeId,
                Description=t.Description,
                Name=t.Name,
                RequiredLevel=t.RequiredLevel,
                RewardPoints=t.RewardPoints,
                UserId=t.UserId,
                UserQuests=t.UserQuests,
                TokenId=t.TokenId,
            };
           await _dbContext.Quests.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return t;
        }

        public async Task<UserQuest> AddUserQuestAsync(UserQuest t)
        {
            await _dbContext.UserQuests.AddAsync(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public async Task<Quest> FindById(Guid id)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Quest quest = await _dbContext.Quests.Select(s => new Quest
            {
                Id= s.Id,
                Name= s.Name,
                Badge= s.Badge,
               BadgeId=s.BadgeId,
                Description= s.Description,
                UserId= s.UserId,
                UserQuests= s.UserQuests,
                RequiredLevel= s.RequiredLevel,
                RewardPoints = s.RewardPoints,
                TokenId=s.TokenId,
          
                
                
            }).FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return quest;
        }

        public async Task<UserQuest> GetUserQuestByUserIdAndQuestIdAsync(Guid userId, Guid questId)
        {
            var userQuest = await _dbContext.UserQuests
                .SingleOrDefaultAsync(uq => uq.UserId == userId && uq.QuestId == questId);

            return userQuest;
        }

        public async Task UpdateUserQuest(UserQuest userQuest)
        {
            _dbContext.UserQuests.Update(userQuest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserQuest> GetUserQuestsAsync(Guid userId, Guid questId)
        {
            return await _dbContext.UserQuests
                .Where(uq => uq.UserId == userId && uq.QuestId == questId).FirstOrDefaultAsync();
        }


    }
}
