using Gamification.Entities;

namespace Gamification.Repositories
{
    public interface IQuestRepository
    {
        Task<UserQuest> AddUserQuestAsync(UserQuest userQuest);
        Task<Quest> FindById(Guid id);
        Task<Quest> ProposeQuestAsync(Quest t);
        Task<List<Quest>> GetQuestsByUserIdAsync(Guid userId);
        Task<UserQuest> GetUserQuestByUserIdAndQuestIdAsync(Guid userId, Guid questId);
        Task UpdateUserQuest(UserQuest userQuest);
        Task<UserQuest> GetUserQuestsAsync(Guid userId, Guid questId);
    }
}
