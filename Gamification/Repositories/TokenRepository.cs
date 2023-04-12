using Gamification.Context;
using Gamification.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamification.Repositories
{
    public class TokenRepository:ITokenRepository
    {


        private readonly GameDbContext _dbContext;

        public TokenRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<List<Token>> GetTokensByUserIdAsync(Guid userId)
        {
            return await _dbContext.UserTokens
                .Where(b => b.UserId == userId)
                .Select(ub => ub.Token)
                .ToListAsync();
        }
       

        public async Task<Token> AddTokenAsync(Token t)
        {
            var entity = new Token()
            {
             
                Id = Guid.NewGuid(),
                UserTokens = t.UserTokens,
                Value = t.Value
            };

           await _dbContext.Tokens.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return t;
        }

        public async Task<Token> GetTokenById(Guid id)
        {


            Token token = await _dbContext.Tokens.Select(s => new Token
            {
               Id=s.Id,
            
               UserTokens= s.UserTokens,
                Value = s.Value
            }).FirstOrDefaultAsync(s => s.Id == id);

            return token;

        }

        public async Task<UserToken> AddUserTokenAsync(UserToken t)
        {
            await _dbContext.UserTokens.AddAsync(t);
            await _dbContext.SaveChangesAsync();

            return t;
        }


    }
}
