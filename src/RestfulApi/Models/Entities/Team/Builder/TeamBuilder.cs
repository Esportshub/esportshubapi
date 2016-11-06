using System;
using System.Collections.Generic;
using RestfulApi.Models.Validator;

namespace EsportshubApi.Models.Entities
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
            throw new NotImplementedException();
        }

        public Team Build(TeamValidator validator)
        {
            validator.Validate(_team);
           return _team;
        }

        public ITeamBuilder SetCreated(DateTime input)
        {
            _team.Created = input;
            return this;
        }

        public ITeamBuilder SetEvents(List<Event> input)
        {
           _team.TeamEvents = input;
           return this;
        }

        public ITeamBuilder SetGame(Group input)
        {
           _team.Game = input;
           return this;
        }

        public ITeamBuilder SetName(string input)
        {
            _team.Name = input;
            return this;
        }

        public ITeamBuilder SetPlayers(List<Player> input)
        {
           _team.Players = input;
           return this;
        }

        public ITeamBuilder SetUpdated(DateTime input)
        {
            _team.Updated = input;
            return this;
        }

        public ITeamBuilder TeamGuid(Guid input)
        {
            _team.TeamGuid = input;
            return this;
        }

        public ITeamBuilder TeamId(int input)
        {
            _team.TeamId = input;
            return this;
        }
    }
}