using System;
using System.Collections.Generic;
using Extensions.Entities;
namespace Models.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        public Guid PlayerGuid { get; set; }
        public string Nickname { get; set; }
        public Account Account { get; set; }
        public List<Player> Followers { get; set; }
        public List<Game> Games { get; set; }
        public List<Team> Teams { get; set; }
        public List<Integration> Integrations { get; set; }
        public List<Activity> Activities { get; set; }

        public override bool Equals(object obj)
        {
            Player objPlayer = (Player)obj;

            if (this.CompareEntities(obj))
                return objPlayer.PlayerId == this.PlayerId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

