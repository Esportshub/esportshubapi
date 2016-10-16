using System;
using System.Collections.Generic;
using Extensions.Entities;

namespace Models.Entities
{
    public class Game
    {
        private Game (){} 
        public static GameBuilder Builder(){
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


        public override bool Equals(object obj)
        {
            Game objPlayer = (Game)obj;

            if (this.CompareEntities(obj))
                return objPlayer.GameId == this.GameId;

            return false;
        }
         public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}