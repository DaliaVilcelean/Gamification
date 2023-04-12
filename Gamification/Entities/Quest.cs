namespace Gamification.Entities
{
    public class Quest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredLevel { get; set; }
        public int RewardPoints { get; set; }
        public Guid BadgeId { get; set; }
        public Guid UserId { get; set; }
        public Guid TokenId { get; set; }

        public virtual Badge Badge { get; set; }
        
        public virtual List<UserQuest> UserQuests { get; set; }
    }
}
