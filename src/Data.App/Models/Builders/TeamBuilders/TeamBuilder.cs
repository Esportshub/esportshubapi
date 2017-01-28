using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
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

        public ITeamBuilder SetTeamEvents(List<Event> events)
        {
            _team.Events = events;
            return this;
        }
         
        public ITeamBuilder SetGame(Game game)
        {
           _team.Game = game;
           return this;
        }

        public ITeamBuilder SetName(string name)
        {
            _team.Name = name;
            return this;
        }

        public ITeamBuilder SetPlayerTeams(List<PlayerTeams> playerTeams)
        {
           _team.PlayerTeams = playerTeams;
           return this;
        }

        public ITeamBuilder TeamGuid(Guid guid)
        {
            _team.TeamGuid = guid;
            return this;
        }
    }
}