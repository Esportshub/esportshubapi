using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.TeamBuilders
{
    public class TeamBuilder : ITeamBuilder
    {
        private Team _team;

        public TeamBuilder (Team team)
        {
          _team = team;
        }

        public Team Build()
        {
            return _team;
        }

        public ITeamBuilder SetTeamEvents(List<TeamEvent> input)
        {
            _team.TeamEvents = input;
            return this;
        }
         
        public ITeamBuilder SetGame(Game input)
        {
           _team.Game = input;
           return this;
        }

        public ITeamBuilder SetName(string input)
        {
            _team.Name = input;
            return this;
        }

        public ITeamBuilder SetPlayerTeams(List<PlayerTeams> input)
        {
           _team.PlayerTeams = input;
           return this;
        }

        public ITeamBuilder TeamGuid(Guid input)
        {
            _team.TeamGuid = input;
            return this;
        }
    }
}