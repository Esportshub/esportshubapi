using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApi.App.Extensions.Entities;
using RestfulApi.App.Models.Esportshub.Builders.TeamBuilders;
using RestfulApi.App.Models.Esportshub.Entities.Events;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;

namespace RestfulApi.App.Models.Esportshub.Entities
{

    public class Team : IEsportshubEntity
    {

        private Team() { }

        public static TeamBuilder Builder()
        {
            return new TeamBuilder(new Team());
        }
        public int TeamId { get; private set; }

        public string Name { get; set; }

        public Guid TeamGuid { get; set; }

        public DateTime Created { get; private set; }

        public DateTime Updated { get; private set; }

        public Game Game { get; set; }

        public List<TeamEvent> TeamEvents { get; set; }

        public List<PlayerTeams> PlayerTeams { get; set; }

        [NotMapped]
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