using Gamification.Entities;

namespace Gamification.DTOs
{
    public class UserDTOs
    {
       
       public Guid  Id { get; set; }
       public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
         public string LastName { get; set; }
          public bool IsAdmin { get; set; }
        public string Type { get; set; }

    
    }

    public class LogInDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
    }

    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int nrTokens { get; set; }
        public List<BadgeDTO> Badges { get; set; }
        public List<TokenDTO> Tokens { get; set; }
        public List<QuestDTO> Quests { get; set; }
    }
}
