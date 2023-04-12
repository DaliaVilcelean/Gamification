using Gamification.DTOs;
using Gamification.Entities;
using Gamification.Repositories;
using Gamification.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardsController : ControllerBase
    {

        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
           _rewardService= rewardService;
        }


        [HttpPost]
        [Route("AddReward")]
        public async Task<ActionResult> AddReward([FromBody] Rewards t)
        {

            var entity = new Rewards()
            {
                Id=Guid.NewGuid(),
                Description=t.Description,
                Name=t.Name,
                PointsNeeded=t.PointsNeeded,
            };
            var user1 = await _rewardService.AddReward(entity);
            return Ok(user1);

        }


        [HttpPost]
        [Route("BuyReward/{userId}/{itemId}")]
        public async Task<ActionResult> BuyRewardAsync(Guid userId, Guid itemId)
        {
            var reward=await _rewardService.BuyItemAsync(userId, itemId); 
            return Ok(reward);

        }

    }
}
