using Microsoft.AspNetCore.Identity;

namespace Gamification.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
       public string Type { get; set; }
        public int NrTokens { get; set; }
        public virtual List<UserBadge>? UserBadges { get; set; }
        public virtual List<UserToken>? UserTokens { get; set; }
        public virtual List<UserQuest>? UserQuests { get; set; }
        public virtual List<UserRewards> UserRewards { get; set; }




    }
}
