using Gamification.Context;
using Gamification.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Repositories
{
    public class BadgeRepository:IBadgeRepository
    {

        private readonly GameDbContext _dbContext;

        public BadgeRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Badge>> GetBadgesByUserIdAsync(Guid userId)
        {
            return await _dbContext.UserBadges
                .Where(b => b.UserId == userId)
                .Select(ub=>ub.Badge)
                .ToListAsync();
        }

        public async Task<Badge> AddBadgeAsync(Badge t)
        {

            var entity = new Badge() { 
            Id=Guid.NewGuid(),
            Description= t.Description,
            Name=t.Name,
            Points=t.Points,
            UserBadges=t.UserBadges,
            UserId=t.UserId,
            };
          await  _dbContext.Badges.AddAsync(t);
            await _dbContext.SaveChangesAsync();

            return t;
        }

   

   

        public async Task<Badge> FindById(Guid id)
        {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Badge badge = await _dbContext.Badges.Select(s => new Badge
            {
                Id = s.Id,
                UserId = s.UserId,
                Points=s.Points,
                UserBadges=s.UserBadges,
                Description= s.Description,
                Name= s.Name,
                
            }).FirstOrDefaultAsync(s => s.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            return badge;

        }

        public async Task<UserBadge> AddUserBadgeAsync(UserBadge t)
        {
            await _dbContext.UserBadges.AddAsync(t);
            await _dbContext.SaveChangesAsync();

            return t;
        }

        



    }
}
