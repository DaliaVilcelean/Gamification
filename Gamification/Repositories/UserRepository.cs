using Gamification.Context;
using Gamification.DTOs;
using Gamification.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Gamification.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly GameDbContext _dbContext;
        

        public UserRepository(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _dbContext.Users.Select(s => new User
            {
                Id = s.Id,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                IsAdmin = s.IsAdmin,
                Password = s.Password,
               Type=s.Type,
              
            }).Where(s => s.Email == username).Where(s => s.Password == password)
            .FirstOrDefaultAsync();


            return user;

        }
        public async Task<List<User>> GetUsersByEmailAsync(string email)
        {
            return await _dbContext.Users
                .Where(u => u.Email == email)
                .ToListAsync();
        }
        public async Task<User> Create(User t)
        {

            var entity = new User()
            {
                Id = Guid.NewGuid(),
                Email = t.Email,
                FirstName = t.FirstName,
                LastName = t.LastName,
                IsAdmin = t.IsAdmin,
                Password = t.Password,
                Type = t.Type,
                NrTokens= t.NrTokens,
                UserBadges= t.UserBadges,
                UserQuests= t.UserQuests,
                UserTokens= t.UserTokens,
            };
           
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(User t)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> FindAll()
        {
      return await _dbContext.Users.Include(u => u.UserBadges)
            .ThenInclude(ub => ub.Badge)
        .Include(u => u.UserTokens)
            .ThenInclude(ut => ut.Token)
        .Include(u => u.UserQuests)
            .ThenInclude(uq => uq.Quest)
        .ToListAsync();
        }

        public async Task<User> FindById(Guid id)
        {
            User user = await _dbContext.Users.Select (s => new User
           {
               Id = s.Id,
              FirstName=s.FirstName,
              LastName=s.LastName,
              IsAdmin = s.IsAdmin,  
              Password = s.Password,
              Type=s.Type,
              Email=s.Email,
              NrTokens=s.NrTokens,
              UserBadges=s.UserBadges,
              UserQuests=s.UserQuests,
              UserTokens=s.UserTokens
           }).FirstOrDefaultAsync(s => s.Id == id);
            return user;
        }

        public async Task<User> Update(User t)
        {
            var existingUser = await _dbContext.Users
        .FirstOrDefaultAsync(u => u.Id == t.Id);


            if (existingUser == null)
            {
                return null;
            }
           existingUser.NrTokens = t.NrTokens;
          _dbContext.Update(existingUser);

           
            await _dbContext.SaveChangesAsync();

            return existingUser;

        }

        public async Task UpdateUserNrTokensAsync(User t)
        {
            User user = await _dbContext.Users.Select(s => new User
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                IsAdmin = s.IsAdmin,
                Password = s.Password,
                Type = s.Type,
                Email = s.Email,
                NrTokens = s.NrTokens,
                UserBadges = s.UserBadges,
                UserQuests = s.UserQuests,
                UserTokens = s.UserTokens
            }).FirstOrDefaultAsync(s => s.Id == t.Id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

         
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
           
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
