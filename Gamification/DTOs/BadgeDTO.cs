using Gamification.Entities;

namespace Gamification.DTOs
{
    public class BadgeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
    }
    public class TokenDTO
    {
        public Guid Id { get; set; }
        public int Value { get; set; }

    }
    public class QuestDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardPoints { get; set; }
        public Guid UserId { get; set; }
        public Guid BadgeId { get; set; }
        public Guid TokenId { get; set; }

    }

    public class UserQuestDTO
    {
        public Guid QuestId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CompletionDate { get; set; }
        public int Reward { get; set; }
    }
}
