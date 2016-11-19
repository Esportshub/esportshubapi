using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;
using EsportshubApi.Models.Entities.mappings;

namespace EsportshubApi.Models.Entities
{
    public class Game : EsportshubEntity
    {
        private Game() { }
        public static GameBuilder Builder()
        {
            return new GameBuilder(new Game());
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public Guid GameGuid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<Team> Teams { get; set; }
        public List<GameEvent> GameEvents { get; set; }
        public List<PlayerGames> PlayerGames { get; set; }

        public int Id => GameId;

        public override bool Equals(object obj)
        {
            Game game = (Game)obj;

            if (this.CompareEntities(obj))
                return game.GameId == this.GameId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}