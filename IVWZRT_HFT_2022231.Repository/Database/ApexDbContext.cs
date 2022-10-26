using Microsoft.EntityFrameworkCore;

using IVWZRT_HFT_2022231.Models;

namespace IVWZRT_HFT_2022231.Repository
{
    public class ApexDbContext : DbContext
    {
        public ApexDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // A player can play multiple matches, and a match contains multiple players
            modelBuilder.Entity<Player>()
                .HasMany(p => p.Matches)
                .WithMany(m => m.Players)
                .UsingEntity<EndGameStat>(
                    s => s.HasOne(s => s.Match).WithMany().HasForeignKey(s => s.MatchId).OnDelete(DeleteBehavior.Cascade),
                    s => s.HasOne(s => s.Player).WithMany().HasForeignKey(s => s.PlayerId).OnDelete(DeleteBehavior.Cascade)
                );

            // An endgame stat belongs to 1 player
            modelBuilder.Entity<EndGameStat>()
                .HasOne(s => s.Player)
                .WithMany(p => p.Stats)
                .HasForeignKey(s => s.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

            // An endgame stat is unique in a match
            modelBuilder.Entity<EndGameStat>()
                .HasOne(s => s.Match)
                .WithMany(m => m.Stats)
                .HasForeignKey(s => s.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // A player mains 1 legend, and a legend can be mained by multiple players
            modelBuilder.Entity<Player>()
                .HasOne(p => p.Main)
                .WithMany(l => l.Players)
                .HasForeignKey(p => p.LegendId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                    .UseInMemoryDatabase("apex")
                    .UseLazyLoadingProxies();
            }
        }

        // Tables
        public DbSet<EndGameStat> Stats { get; set; }
        public DbSet<Legend> Legends { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
