using System.Linq;
using EsportshubApi.Models.Entities;

namespace EsportshubApi.Models
{
    public static class EsportshubExtensions
    {
        public static void EnsureSeedData(this EsportshubContext context)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Players.Any())
                {
                    var player = Player.Builder()
                        .SetNickname("Sjuften").
                        Build();
                    context.Players.Add(player);
                    context.SaveChanges();
                }
            }
        }
    }
}