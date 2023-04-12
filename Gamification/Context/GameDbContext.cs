using Gamification.Entities;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;


namespace Gamification.Context
{
    public class GameDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Badge> Badges { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<UserQuest> UserQuests { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserBadge> UserBadges { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Rewards> Rewards { get; set; }
        public DbSet<UserRewards> UserRewards { get;  set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
        modelBuilder.Entity<User>()
                .HasMany(u => u.UserBadges)
                .WithOne(ub => ub.User)
                .HasForeignKey(ub => ub.UserId);
           


            modelBuilder.Entity<Badge>()
      .HasMany(b => b.UserBadges)
      .WithOne(ub => ub.Badge)
      .HasForeignKey(ub => ub.BadgeId);


            modelBuilder.Entity<Token>()
    .HasMany(t => t.UserTokens)
    .WithOne(ut => ut.Token)
    .HasForeignKey(ut => ut.TokenId);

            // Configure Quest entity
            modelBuilder.Entity<Quest>()
                .HasMany(q => q.UserQuests)
                .WithOne(uq => uq.Quest)
                .HasForeignKey(uq => uq.QuestId);



            modelBuilder.Entity<UserToken>()
      .HasKey(ut => new { ut.Id }) ;



            modelBuilder.Entity<UserToken>()
      .HasOne(ut => ut.User)
      .WithMany(u => u.UserTokens)
      .HasForeignKey(ut => ut.UserId)
      .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<UserToken>()
                .HasOne(ut => ut.Token)
                .WithMany(t => t.UserTokens)
                .HasForeignKey(ut => ut.TokenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRewards>()
        .HasKey(ur => ur.Id);

            modelBuilder.Entity<UserRewards>()
                .HasOne(ur => ur.Reward)
                .WithMany()
                .HasForeignKey(ur => ur.RewardId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserRewards>()
       .HasOne(ur => ur.User)
       .WithMany(u => u.UserRewards)
       .HasForeignKey(ur => ur.UserId)
       .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<UserQuest>()
       .HasKey(uq => new { uq.Id });

            modelBuilder.Entity<UserQuest>()
                .HasOne(uq => uq.User)
                .WithMany(q => q.UserQuests)
                .HasForeignKey(uq => uq.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserQuest>()
                .HasOne(uq => uq.Quest)
                .WithMany(q => q.UserQuests)
                .HasForeignKey(uq => uq.QuestId).OnDelete(DeleteBehavior.Restrict); 


            modelBuilder.Entity<UserBadge>()
        .HasKey(ub => new { ub.Id });

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId).OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.BadgeId).OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
