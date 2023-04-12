using Gamification.Entities;
using Gamification.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Service
{
    public class RewardService:IRewardService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRewardRepository _rewardRepository;   

        public RewardService(IUserRepository userRepository,IRewardRepository rewardRepository)
        {
        _rewardRepository= rewardRepository;
            _userRepository= userRepository;
        }  

        public async Task<Rewards> AddReward(Rewards r)
        {
            var reward = await _rewardRepository.Create(r);

            return reward;
        }



        public async Task<UserRewards> BuyItemAsync(Guid userId, Guid itemId)
        {
            // Verificăm dacă utilizatorul este autentificat și are suficiente token-uri pentru a cumpăra articolul
            var user = await _userRepository.FindById(userId);
            var item = await _rewardRepository.FindById(itemId);

            if (user == null || item == null)
            {
                throw new ArgumentException($"User with ID {userId} does not exist");
            }

            if (user.NrTokens < item.PointsNeeded)
            {
                throw new ArgumentException($"Userul nu are suficienti Tokens");
            }

            // Actualizăm numărul de token-uri al utilizatorului și salvăm tranzacția în baza de date
            user.NrTokens -= item.PointsNeeded;
            await _userRepository.Update(user);

            var reward = new UserRewards()
            {
               
              
                UserId= userId,
                RewardId=itemId,
                Date= DateTime.UtcNow,
                Id=Guid.NewGuid(),
            };
            await _rewardRepository.AddUserRewardAsync(reward);
            

            return reward;
        }



    }
}
