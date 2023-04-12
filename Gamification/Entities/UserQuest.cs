namespace Gamification.Entities
{
    public class UserQuest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid QuestId { get; set; }
        public Quest Quest { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid TokenId { get; set; }
        public Token Token { get; set; }
        public Guid BadgeId { get; set; }
        public Badge Badge { get; set; }
        public int Reward { get; set; }
        public Status Status { get; set; }
        public string Proof { get; set; }
    }

    public enum Status
    {
        Completed,
        Pending,
        Done
    }
}
