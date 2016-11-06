using EsportshubApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsportshubApi.Models 
{
    public class EsportshubContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Games { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<TeamEvent> TeamEvents { get; set; }
        public DbSet<GameEvent> GameEvent { get; set; }

        public EsportshubContext(DbContextOptions<EsportshubContext> options) : base(options) {}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Account>().HasOne(a => a.Player);

            modelBuilder.Entity<Player>().HasOne(p => p.Account);
            modelBuilder.Entity<Player>().HasMany(p => p.Games);
            modelBuilder.Entity<Player>().HasMany(p => p.Activities);
            modelBuilder.Entity<Player>().HasMany(p => p.Integrations);
            modelBuilder.Entity<Player>().HasMany(p => p.Teams);
            modelBuilder.Entity<Player>().HasMany(p => p.Groups);
            modelBuilder.Entity<Player>().HasMany(p => p.Followers);

            modelBuilder.Entity<Team>().HasMany(t => t.TeamEvents);
            modelBuilder.Entity<Team>().HasOne(t => t.Game);
            modelBuilder.Entity<Team>().HasMany(t => t.Players);

            modelBuilder.Entity<Activity>().HasOne(a => a.Player);

            modelBuilder.Entity<GameEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GameEvent>().HasOne(e => e.Game);

            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Team);

            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Group);

            modelBuilder.Entity<Group>().HasMany(g => g.Players);
            //modelBuilder.Entity<Group>().HasMany(g => g.Teams);
           // modelBuilder.Entity<Group>().HasMany(g => g.GroupEvents);

            modelBuilder.Entity<Integration>().HasOne(i => i.Player);
        }
    }
}