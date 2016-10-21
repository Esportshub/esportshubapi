using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;

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
        public List<Event> GameEvents { get; set; }
        public List<Player> Players { get; set; }

        public int Id
        {
            get
            {
                return GameId;
            }
        }

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