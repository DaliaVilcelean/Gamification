namespace Gamification.Entities
{
    public class UserBadge
    {
      
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BadgeId { get; set; }
        public virtual User User { get; set; }
        public virtual Badge Badge { get; set; }
    }
}
