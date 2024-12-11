namespace FootballClubApp
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class FootballClubContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchTeam> MatchTeams { get; set; }
        public DbSet<League> Leagues { get; set; }

        public FootballClubContext(DbContextOptions<FootballClubContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacja wiele-do-wiele między Match i Team
            modelBuilder.Entity<MatchTeam>()
                .HasKey(mt => new { mt.MatchId, mt.TeamId });

            modelBuilder.Entity<MatchTeam>()
                .HasOne(mt => mt.Match)
                .WithMany(m => m.MatchTeams)
                .HasForeignKey(mt => mt.MatchId);

            modelBuilder.Entity<MatchTeam>()
                .HasOne(mt => mt.Team)
                .WithMany(t => t.MatchTeams)
                .HasForeignKey(mt => mt.TeamId);

            // Relacja jeden-do-jeden między Team a Coach
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithMany(c => c.Teams)
                .HasForeignKey(t => t.CoachId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
