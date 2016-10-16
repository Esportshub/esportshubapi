using System;
using System.Collections.Generic;

namespace Models.Entities 
{
    public class Game 
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public Guid GameGuid { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<Team> Teams { get; set; }
        public List<Event> GameEvents { get; set;}
        public List<Player> Players { get; set;}
    }
}