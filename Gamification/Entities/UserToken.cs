namespace Gamification.Entities
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TokenId { get; set; }
        public virtual User? User { get; set; }
        public virtual Token? Token { get; set; }
    }
}
