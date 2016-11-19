using System;
using System.Collections.Generic;
using EsportshubApi.Extensions;
using EsportshubApi.Models.Entities.mappings;

namespace EsportshubApi.Models.Entities
{

    public class Team : EsportshubEntity
    {

        private Team() { }

        public static TeamBuilder Builder()
        {
            return new TeamBuilder(new Team());
        }
        public int TeamId { get; set; }

        public string Name { get; set; }

        public Guid TeamGuid { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public Game Game { get; set; }

        public List<TeamEvent> TeamEvents { get; set; }

        public List<PlayerTeams> PlayerTeams { get; set; }

        public int Id => TeamId;

        public override bool Equals(object obj)
        {
            Team team = (Team)obj;

            if (this.CompareEntities(obj))
                return team.TeamId == this.TeamId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}