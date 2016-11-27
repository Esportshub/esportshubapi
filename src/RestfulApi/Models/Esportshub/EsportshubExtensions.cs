﻿using System.Collections.Generic;
using System.Linq;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Entities.mappings;

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
<<<<<<< HEAD
//                    var player = Player.Builder()
//                        .SetNickname("Sjuften").
//                        Build();
//                    context.Players.Add(player);
//                    context.SaveChanges();
=======
                    List<Game> games = new List<Game>()
                    {
                        Game.Builder().SetName("Starcraft 2").Build(),
                        Game.Builder().SetName("League of legends").Build(),
                        Game.Builder().SetName("Global Offensive").Build(),
                        Game.Builder().SetName("Dota 2").Build()
                    };
                    List<PlayerGames> playerGames = new List<PlayerGames>()
                    {

                    };
                    var Admin = context.ApplicationUsers.First();


                    var player = Player.Builder()
                        .SetNickname("DenLilleMand").SetAccount(Admin).SetPlayerGames(games).Build();
                    context.Players.Add(player);
                    context.SaveChanges();
>>>>>>> a7fb2e427bec348e1e1213151d099822e3895f4e
                }
            }
        }
    }
}