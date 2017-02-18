using System.IO;
using System.Linq;
using System.Reflection;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.App.Models
{
    public class EsportshubContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Group> Games { get; set; }
        public DbSet<Integration> Integrations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<EsportshubEvent> EsportshubEvents { get; set; }
        public DbSet<Group> Groups { get; set; }

        public EsportshubContext(DbContextOptions<EsportshubContext> options) : base(options)
        {
        }

        public EsportshubContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()) + "/Data.App")
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var connString = config["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //One-to-one
            modelBuilder.Entity<Group>().HasOne(g => g.Role);
            modelBuilder.Entity<SocialMedia>()
                .HasOne(sm => sm.Integration)
                .WithOne(i => i.SocialMedia)
                .HasForeignKey<Integration>(i => i.SocialMediaForeignKey);


            //one-to-many
            modelBuilder.Entity<Player>().HasMany(p => p.Integrations).WithOne(i => i.Player);
            modelBuilder.Entity<Player>().HasMany(p => p.Followers);
            modelBuilder.Entity<Player>().HasMany(p => p.Activities).WithOne(a => a.Player);
            modelBuilder.Entity<Game>().HasMany(g => g.Teams).WithOne(t => t.Game);

            //many to many
            modelBuilder.Entity<PlayerGames>().HasKey(pg => new {pg.PlayerId, pg.GameId});
            modelBuilder.Entity<PlayerTeams>().HasKey(pt => new {pt.PlayerId, pt.TeamId});
            modelBuilder.Entity<PlayerGroups>().HasKey(pg => new {pg.PlayerId, pg.GroupId});

            //game
            modelBuilder.Entity<Game>()
                .Property(g => g.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Game>()
                .Property(g => g.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            //player
            modelBuilder.Entity<Player>()
                .Property(p => p.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Player>()
                .Property(p => p.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            //group
            modelBuilder.Entity<Group>()
                .Property(g => g.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>()
                .Property(g => g.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            //activity
            modelBuilder.Entity<Activity>()
                .Property(a => a.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Activity>()
                .Property(a => a.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            //integration
            modelBuilder.Entity<Integration>()
                .Property(i => i.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Integration>()
                .Property(i => i.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();

            //team
            modelBuilder.Entity<Team>()
                .Property(t => t.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Team>()
                .Property(t => t.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();


            //role
            modelBuilder.Entity<Group.Roles>()
                .Property(r => r.Created)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Group.Roles>()
                .Property(r => r.Updated)
                .HasDefaultValueSql("getutcdate()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}