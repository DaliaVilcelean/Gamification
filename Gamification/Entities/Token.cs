using Gamification.DTOs;

namespace Gamification.Entities
{
    public class Token
    {
        public Guid Id { get; set; }
       
        public int Value { get; set; }

        public virtual List<UserToken> UserTokens { get; set; }

     
    }
}
