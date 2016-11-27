﻿using System.Collections.Generic;
using System.Linq;
using EsportshubApi.Models;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Models.Esportshub.Entities.Player;

namespace RestfulApi.Models.Esportshub
{
    public static class EsportshubExtensions
    {
        public static void EnsureSeedData(this EsportshubContext context)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Players.Any() && context.ApplicationUsers.Any())
                {
                    List<Game> games = new List<Game>()
                    {
                        Game.Builder().SetName("Starcraft 2").Build(),
                        Game.Builder().SetName("League of legends").Build(),
                        Game.Builder().SetName("Global Offensive").Build(),
                        Game.Builder().SetName("Dota 2").Build()
                    };
                    var admin = context.ApplicationUsers.First();
                    var player = Player.Builder().SetNickname("DenLilleMand").SetAccount(admin);
                    List<PlayerGames> playerGames = new List<PlayerGames>();
                    foreach (var game in games)
                    {
                        playerGames.Add(new PlayerGames()
                        {
                            Player = player.Build(),
                            Game = game
                        });
                    }
                    player.SetPlayerGames(playerGames);
                    context.Players.Add(player.Build());
                    context.SaveChanges();
                }
            }
        }
    }
}