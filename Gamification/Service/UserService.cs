using AutoMapper;
using Gamification.DTOs;
using Gamification.Entities;
using Gamification.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Service
{
    public class UserService:IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IQuestRepository _questRepository;

        public UserService(IUserRepository userRepository,
            IQuestRepository questRepository,
            ITokenRepository tokenRepository,
            IBadgeRepository badgeRepository)
        {
            _userRepository = userRepository;
            _questRepository = questRepository;
            _badgeRepository= badgeRepository;
            _tokenRepository= tokenRepository;
        }

        public async Task<LoginResponseDTO> LoginAsync(string email, string password)
        {
            var user = await _userRepository.Login(email, password);

            if (user == null )
            {
                return null;
            }

            var user2 = new LoginResponseDTO
            {
                Email = user.Email,
                Id=user.Id,
                Type=user.Type
              
            };

            return user2;
        }

        public async Task<UserDTOs> SignupAsync(User t)
        {
          
          

        var user=  await _userRepository.Create(t);
           return  new UserDTOs
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                Type =  user.Type,
            };

            
        }

        public async Task<List<UserDTO>> GetAllUsersWithBadgesTokensQuestsAsync()
        {
            var users =await _userRepository.FindAll();
            var userDTOs=new List<UserDTO>();   

       foreach(var user in users)
            {
                var badges = await _badgeRepository.GetBadgesByUserIdAsync(user.Id);
                var tokens = await _tokenRepository.GetTokensByUserIdAsync(user.Id);
                var quests = await _questRepository.GetQuestsByUserIdAsync(user.Id);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDTO>();
                    cfg.CreateMap<Badge, BadgeDTO>();
                    cfg.CreateMap<Token, TokenDTO>();
                    cfg.CreateMap<Quest, QuestDTO>();
                });
                var _mapper = config.CreateMapper();


                var userDetailsDto = _mapper.Map<UserDTO>(user);
                userDetailsDto.Badges = _mapper.Map<List<BadgeDTO>>(badges);
                userDetailsDto.Tokens = _mapper.Map<List<TokenDTO>>(tokens);
                userDetailsDto.Quests = _mapper.Map<List<QuestDTO>>(quests);

                user.NrTokens = 0;
                userDetailsDto.nrTokens = 0;
                foreach(var token in tokens)
                {
                    userDetailsDto.nrTokens += token.Value;
                }

                user.NrTokens = userDetailsDto.nrTokens;
                await _userRepository.Update(user);
                userDTOs.Add(userDetailsDto);
              
            }
          
            return userDTOs.OrderByDescending(u => u.Badges.Count())
             .ToList();

        }

        public async Task<UserDTO> FindById(Guid id)
        {
            var user = await _userRepository.FindById(id);
            var userTO = new UserDTO
            {
                Id = id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Badges = user.UserBadges.Select(u => new BadgeDTO
                {
                    Description = u.Badge.Description,
                    Id = u.Badge.Id,
                    Name = u.Badge.Name,
                    Points = u.Badge.Points
                }).ToList(),
                Quests = user.UserQuests.Select(u => new QuestDTO
                {
                    Id = u.Quest.Id,
                    Name = u.Quest.Name,
                    Description = u.Quest.Description,
                    RewardPoints = u.Quest.RewardPoints,
                    UserId = u.Quest.UserId,
                }).ToList(),
                Tokens = user.UserTokens.Select(u => new TokenDTO
                {
                    Id = u.Token.Id,
                  
                    Value = u.Token.Value
                }).ToList(),

            };
            return userTO;
        }

        public async Task<UserDTO>  GetUserDetailsAsync(Guid userId)
        {
            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                return null;
            }

            var badges = await _badgeRepository.GetBadgesByUserIdAsync(userId);
            var tokens = await _tokenRepository.GetTokensByUserIdAsync(userId);
            var quests = await _questRepository.GetQuestsByUserIdAsync(userId);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Badge, BadgeDTO>();
                cfg.CreateMap<Token, TokenDTO>();
                cfg.CreateMap<Quest, QuestDTO>();
            });
            var _mapper = config.CreateMapper();

            var userDetailsDto = _mapper.Map<UserDTO>(user);
            userDetailsDto.Badges = _mapper.Map<List<BadgeDTO>>(badges);
            userDetailsDto.Tokens = _mapper.Map<List<TokenDTO>>(tokens);
            userDetailsDto.Quests = _mapper.Map<List<QuestDTO>>(quests);

        

            return userDetailsDto;

        }


    }
}
