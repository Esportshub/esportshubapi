using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;

namespace EsportshubApi.Models.Entities
{
    public class Player
    {
        public Player() { }

        public int PlayerId { get; set; }

        public Guid PlayerGuid { get; set; }

        public string Nickname { get; set; }

        public Account Account { get; set; }

        public List<Player> Followers { get; set; }

        public List<Game> Games { get; set; }

        public List<Team> Teams { get; set; }
        public List<Integration> Integrations { get; set; }
        public List<Activity> Activities { get; set; }

        public List<Group> Groups { get; set; }

        public override bool Equals (object obj)
        {
            Player objPlayer = (Player)obj;

            if (this.CompareEntities(obj))
                return objPlayer.PlayerId == this.PlayerId;

            return false;
        }

        /**@TODO: Test if this hashcode works in hashmap */
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

