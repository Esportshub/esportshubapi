using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.TeamBuilders
{
    public class TeamBuilder : ITeamBuilder
    {
        private readonly Team _team;

        public TeamBuilder(Team team)
        {
            _team = team;
        }

        public Team Build()
        {
            return _team;
        }

        public ITeamBuilder SetEsportshubEvents(List<EsportshubEvent> esportshubEvents)
        {
            _team.EsportshubEvents = esportshubEvents;
            return this;
        }
<<<<<<< HEAD
         
        public ITeamBuilder SetGame(Game game)
        {
           _team.Game = game;
           return this;
=======

        public ITeamBuilder SetGame(Game input)
        {
            _team.Game = input;
            return this;
>>>>>>> origin/master
        }

        public ITeamBuilder SetName(string name)
        {
            _team.Name = name;
            return this;
        }

        public ITeamBuilder SetPlayerTeams(List<PlayerTeams> playerTeams)
        {
<<<<<<< HEAD
           _team.PlayerTeams = playerTeams;
           return this;
=======
            _team.PlayerTeams = input;
            return this;
>>>>>>> origin/master
        }

        public ITeamBuilder TeamGuid(Guid guid)
        {
            _team.TeamGuid = guid;
            return this;
        }
    }
}