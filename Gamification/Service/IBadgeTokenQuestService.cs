using Gamification.DTOs;
using Gamification.Entities;

namespace Gamification.Service
{
    public interface IBadgeTokenQuestService
    {

        Task<BadgeDTO> AddBadgeAsync(Badge t);
        Task<TokenDTO> AddTokenAsync(Token t);
        Task<UserToken> GrantTokenAsync(Guid userId, Guid tokenId);
        Task<QuestDTO> ProposeQuestQuestAsync(Quest t,Guid userId);
        Task<UserBadge> GrantBadgeAsync(Guid userId, Guid badgeId);
        
        Task<UserQuest> CompleteQuestAsync(Guid userId, Guid questId);
        Task<bool> UploadQuestProofAsync(Guid userId, Guid questId, string proof);
    }
}
