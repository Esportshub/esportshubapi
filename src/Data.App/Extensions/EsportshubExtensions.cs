using System.Collections.Generic;
using System.Linq;
using Data.App.Models.Entities;

namespace Data.App.Models
{
    public static class EsportshubExtensions
    {
        public static void EnsureSeedData(this EsportshubContext context)
        {
            if (context.Players.Any()) return;
            var players = new List<Player>
            {
                new Player()
                {
                    Nickname = "Martin",
                },
                new Player()
                {
                    Nickname = "Matti",
                }
            };
            context.Players.AddRange(players);
            context.SaveChanges();
        }
    }
}