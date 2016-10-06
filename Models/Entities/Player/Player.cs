using System;
using System.Collections.Generic;

namespace Models.Entities
{


    public class Player
    {
        public int PlayerId { get; set; }
        public Guid PlayerGuid { get; set; }
        public string Nickename { get; set; }
        public Account Account { get; set; }
        public List<Player> Followers { get; set; }

        public List<Game> Games { get; set; }
        public List<Team> Teams { get; set; }

        public List<Integration> Integrations { get; set; } 

        public List<Activity> Activities { get; set; }

    }
}