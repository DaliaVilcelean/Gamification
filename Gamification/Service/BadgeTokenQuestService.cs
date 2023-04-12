using Gamification.DTOs;
using Gamification.Entities;
using Gamification.Repositories;
using System.Text.Json.Serialization;
using System.Text.Json;
using Gamification.Context;

namespace Gamification.Service
{
    public class BadgeTokenQuestService:IBadgeTokenQuestService
    {

        private readonly IBadgeRepository _badgeRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IQuestRepository _questRepository;
        private readonly IUserRepository _userRepository;

        public BadgeTokenQuestService(IUserRepository userRepository, IBadgeRepository badgeRepository, ITokenRepository tokenRepository, IQuestRepository questRepository)
        {
            _badgeRepository = badgeRepository;
            _tokenRepository = tokenRepository;
            _questRepository = questRepository;
            _userRepository= userRepository;
        }


        public async Task<BadgeDTO> AddBadgeAsync(Badge t)
        {

            var user = await _badgeRepository.AddBadgeAsync(t);
            return new BadgeDTO
            {
                Id= Guid.NewGuid(),
                Description= user.Description,
                Name= user.Name,
                Points=user.Points,
               
            };


        }

        public async Task<QuestDTO> ProposeQuestQuestAsync(Quest t,Guid userId)
        {

            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }
            
            if (user.NrTokens < t.RewardPoints)
            {
                throw new Exception("User does not have enough tokens to propose this quest.");
            }
            


            var quest2=  await _questRepository.ProposeQuestAsync(t);

            return new QuestDTO
            {
                Name = quest2.Name,
                Description = quest2.Description,
                RewardPoints = quest2.RewardPoints,
                UserId = user.Id,
                Id = Guid.NewGuid(),
                BadgeId = quest2.BadgeId,
                TokenId=quest2.TokenId
            };

        }

        public async Task<TokenDTO> AddTokenAsync(Token t)
        {

            var user = await _tokenRepository.AddTokenAsync(t);
            return new TokenDTO
            {
                Id= Guid.NewGuid(),
              
                Value= user.Value,
            };


        }

   



        public async Task<UserQuest> CompleteQuestAsync(Guid userId, Guid questId )
        {
            // Check if user exists
            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist");
            }

            // Check if quest exists
            var quest = await _questRepository.FindById(questId);
            if (quest == null)
            {
                throw new ArgumentException($"User with ID {questId} does not exist");
            }

            // Check if user already completed the quest
            var userQuest = await _questRepository.GetUserQuestsAsync(userId, questId);
            if (userQuest != null)
            {
                throw new ArgumentException($"User with ID {userId} has done this quest");
            }
            var badge = await _badgeRepository.FindById(quest.BadgeId);
            if (badge == null)
            {
                throw new ArgumentException($"Badge with ID {quest.BadgeId} does not exist");
            }

            var token = await _tokenRepository.GetTokenById(quest.TokenId);
            if (token == null)
            {
                throw new ArgumentException($"Token with ID {quest.TokenId} does not exist");
            }
       
            var entity=  await _questRepository.AddUserQuestAsync(new UserQuest
            {
                UserId = userId,
                QuestId = quest.Id,
            Reward=quest.Badge.Points,
                BadgeId=badge.Id,
                EndTime=DateTime.UtcNow,
                Status=Status.Pending,
                Id=Guid.NewGuid(),
                TokenId=quest.TokenId,
                Proof="not yet verified"
                
                
            });

        

            return entity;
        }




        public async Task<UserToken> GrantTokenAsync(Guid userId, Guid tokenId)
        {
            var token = await _tokenRepository.GetTokenById(tokenId);
            if (token == null)
            {
                throw new ArgumentException($"Token {tokenId} does not exist.");
            }

            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist.");
            }
            user.NrTokens += token.Value;
       //   await   _userRepository.Update(user);

            

            var userToken = new UserToken()
            {
                UserId = user.Id,
                TokenId = token.Id,
                Id=Guid.NewGuid(),
             
            
            };
            
         var token1=  await _tokenRepository.AddUserTokenAsync(userToken);


            
            return token1;



        }

        public async Task<UserBadge> GrantBadgeAsync(Guid userId, Guid badgeId)
        {

            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                return null;
            }

            var badge = await _badgeRepository.FindById(badgeId);
            if (badge == null)
            {
                return null;
            }

            var badge1 = await _badgeRepository.AddUserBadgeAsync(new UserBadge
            {
                UserId = userId,
                BadgeId = badgeId,
                
                Id=Guid.NewGuid(),
            });

            return badge1;
        }

        public async Task<bool> UploadQuestProofAsync(Guid userId, Guid questId, string proof)
        {
            var user = await _userRepository.FindById(userId);
            var quest = await _questRepository.FindById(questId);

            if (user == null || quest == null)
            {
                return false;
            }

            var userQuest = await _questRepository.GetUserQuestByUserIdAndQuestIdAsync(userId, questId);

            if (userQuest == null)
            {
                return false;
            }

            userQuest.Proof = proof;
            userQuest.Status = Status.Completed;
           var tokens= await GrantTokenAsync(userId, userQuest.TokenId);
          var badges=  await GrantBadgeAsync(userId, userQuest.BadgeId);

            if (user.NrTokens < 15)
            {
              await  GrantBadgeAsync(userId, Guid.Parse("4070AD0A-AC43-49F2-BB60-B3184BC0F753"));
            }else
                 if (user.NrTokens >15 && user.NrTokens<50)
            {
              await  GrantBadgeAsync(userId, Guid.Parse("61743DEE-CD1A-4C78-BA12-D768E4557352"));
            }
            else 
            {
              await  GrantBadgeAsync(userId, Guid.Parse("F2F4D75A-B037-4716-943F-67F1AB6F5064"));
            }

            await _questRepository.UpdateUserQuest(userQuest);

           
            

            return true;
        }

      


        



    }
}
