using Gamification.DTOs;
using Gamification.Entities;
using Gamification.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gamification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {

        private readonly IBadgeTokenQuestService _badgeTokenQuestService;

        public PointsController(IBadgeTokenQuestService badgeTokenQuestService)
        {
            _badgeTokenQuestService= badgeTokenQuestService;
        }


        [HttpPost]
        [Route("AddBadge")]
        public async Task<ActionResult> AddBadge([FromBody] BadgeDTO t)
        {

            var entity = new Badge
            {
               Id= Guid.NewGuid(),
              Name=t.Name,
              Description=t.Description,
              Points=t.Points,
             

            };
            var user1 = await _badgeTokenQuestService.AddBadgeAsync(entity);
            return Ok(user1);

        }

        [HttpPost]
        [Route("AddQuest")]
        public async Task<ActionResult> AddQuest([FromBody] QuestDTO t)
        {

            var entity = new Quest
            {
             Id= Guid.NewGuid(),
             Name=t.Name,
             Description=t.Description,
             UserId=t.UserId,
             BadgeId=t.BadgeId,
             RewardPoints=t.RewardPoints,
             TokenId=t.TokenId,
            
             RequiredLevel=t.RewardPoints,
             


            };
            var user1 = await _badgeTokenQuestService.ProposeQuestQuestAsync(entity,entity.UserId);
            return Ok(user1);

        }


        [HttpPost]
        [Route("AddToken")]
        public async Task<ActionResult> AddToken([FromBody] TokenDTO t)
        {

            var entity = new Token
            {
               Id= t.Id,
              
               Value=t.Value,

            };
            var user1 = await _badgeTokenQuestService.AddTokenAsync(entity);
            return Ok(user1);

        }

        [HttpPost]
        [Route("AddUserToken/{userId}/{tokenId}")]
        public async Task<ActionResult> GrantTokenAsync(Guid userId,Guid tokenId)
        {
          var userToken=  await _badgeTokenQuestService.GrantTokenAsync(userId,tokenId);
            return Ok(userToken);
        }

        [HttpPost]
        [Route("AddUserBadge/{userId}/{badgeId}")]
        public async Task<ActionResult> GrantBadgeAsync(Guid userId, Guid badgeId)
        {
            var userBadge = await _badgeTokenQuestService.GrantBadgeAsync(userId,badgeId);  
            return Ok(userBadge);
        }

        [HttpPost]
        [Route("CompleteQuest/{userId}/{questId}")]
        public async Task<ActionResult> CompleteQuest(Guid userId,Guid questId)
        {

            var userQ =await _badgeTokenQuestService.CompleteQuestAsync(userId, questId);
       //  var userQ=await _badgeTokenQuestService.CompleteQuestAsync(userId,questId);
            return Ok(userQ);

        }

        [HttpPost]
        [Route("UploadQuestProof/{userId}/{questId}/{proof}")]
        public async Task<ActionResult> UploadQuestProof(Guid userId, Guid questId, string proof)
        {
            var result = await _badgeTokenQuestService.UploadQuestProofAsync(userId, questId, proof);

            if (result == false)
            {
                return BadRequest("Failed to upload quest proof.");
            }

            return Ok(result);
        }


    }
}
