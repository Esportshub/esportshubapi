using System;
using System.Security.Principal;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Entities.mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;

namespace EsportshubApi.Models
{
    public class EsportshubContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Games { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupEvent> GroupEvents { get; set; }
        public DbSet<TeamEvent> TeamEvents { get; set; }
        public DbSet<GameEvent> GameEvent { get; set; }

        public EsportshubContext(DbContextOptions<EsportshubContext> options) : base(options) {}
        public EsportshubContext() {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One-to-one
            //modelBuilder.Entity<Account>().HasOne(a => a.Player).WithOne(p => p.Account).HasForeignKey<Player>(p => p.AccountForeignKey);
            modelBuilder.Entity<Group>().HasOne(g => g.Role);//.WithOne(r => r.Group).HasForeignKey<Role>(r => r.GroupForeignKey);
            modelBuilder.Entity<GameEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GameEvent>().HasOne(e => e.Game);
            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Team);
            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Group);

            //one-to-many
            modelBuilder.Entity<Player>().HasMany(p => p.Integrations).WithOne(i => i.Player);
            modelBuilder.Entity<Player>().HasMany(p => p.Followers);
            modelBuilder.Entity<Player>().HasMany(p => p.Activities).WithOne(a => a.Player);
            modelBuilder.Entity<Game>().HasMany(g => g.GameEvents).WithOne(ge => ge.Game);
            modelBuilder.Entity<Game>().HasMany(g => g.Teams).WithOne(t => t.Game);
            modelBuilder.Entity<Team>().HasMany(t => t.TeamEvents).WithOne(te => te.Team);
            modelBuilder.Entity<Group>().HasMany(g => g.GroupEvents);

            //many to many
            modelBuilder.Entity<PlayerGames>().HasKey(pg => new {pg.PlayerId, pg.GameId});
            modelBuilder.Entity<PlayerTeams>().HasKey(pt => new {pt.PlayerId, pt.TeamId});
            modelBuilder.Entity<PlayerGroups>().HasKey(pg => new {pg.PlayerId, pg.GroupId});

            //discriminator values
            modelBuilder.Entity<Event>()
                .HasDiscriminator<string>("Type")
                .HasValue<GameEvent>("GameEvent")
                .HasValue<GroupEvent>("GroupEvent")
                .HasValue<TeamEvent>("TeamEvent");

        }
    }
}