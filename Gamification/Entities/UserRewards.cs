namespace Gamification.Entities
{
    public class UserRewards
    {

        public Guid Id { get; set; }
        public Guid RewardId { get; set; }
        public virtual Rewards Reward { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime Date { get; set; }

    }
}
