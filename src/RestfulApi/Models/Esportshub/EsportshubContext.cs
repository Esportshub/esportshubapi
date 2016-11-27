using System;
using System.Threading.Tasks;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Entities.mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EsportshubApi.Models
{
    public class EsportshubContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public EsportshubContext(DbContextOptions<EsportshubContext> options) : base(options) {}
        public EsportshubContext() {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One-to-one
            modelBuilder.Entity<Group>().HasOne(g => g.Role);//.WithOne(r => r.Group).HasForeignKey<Role>(r => r.GroupForeignKey);
            modelBuilder.Entity<GameEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GameEvent>().HasOne(e => e.Game);
            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<TeamEvent>().HasOne(e => e.Team);
            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Event);
            modelBuilder.Entity<GroupEvent>().HasOne(e => e.Group);
            modelBuilder.Entity<Player>().HasOne(p => p.ApplicationUser).WithOne(p => p.Player).HasForeignKey<Player>(p => p.AccountForeignKey);
            modelBuilder.Entity<Game>().Property(g => g.Created).HasDefaultValueSql("getutcdate()").IsRequired();
            modelBuilder.Entity<Game>().Property(g => g.Updated).HasComputedColumnSql("getutcdate()").IsRequired();


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

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration) {
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            string username = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null) {
                if (await roleManager.FindByNameAsync(role) == null) {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                ApplicationUser user = new ApplicationUser {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}