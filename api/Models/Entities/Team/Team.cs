using System;
using System.Collections.Generic;

namespace Models.Entities {

    public class Team {
                public int TeamId { get; set; }

                public string Name { get; set; }
                
                public Guid Guid { get; set; }
            
                public DateTime Created { get; set; }
            
                public DateTime Updated { get; set; }

                public Game Game { get; set; }

                public List<Event> TeamEvents { get; set; }

                public List<Player> Players { get; set; }
    }
}