using System.Collections.Generic;
using System.Linq;
using Data.App.Models;
using Data.App.Models.Entities;

namespace Data.App.Extensions
{
    public static class EsportshubExtensions
    {
        public static void EnsureSeedData(this EsportshubContext context)
        {
            if (context.Players.Any()) return;
            var players = new List<Player>
            {
            };
            context.Players.AddRange(players);
            context.SaveChanges();
        }
    }
}