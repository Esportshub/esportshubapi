using System;
using System.Collections.Generic;
using Models.Entites;

namespace Models.Entities
{

    public class Group
    {

        public int GroupId { get; set; }
        public Guid GroupGuid { get; set; }
        public string Name { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Visibilty Visibilty { get; set; }
        


        public List<Role> Roles { get; set; }
        public List<Player> Players { get; set; }


    }

}