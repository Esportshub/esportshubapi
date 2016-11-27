using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Models.Validator;

namespace RestfulApi.Models.Esportshub.Entities.Team.Builder
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


        public Team Build(IValidator<Team> validator)
        {
            throw new NotImplementedException();
        }


        public Team Build(TeamValidator validator)
        {
            validator.Validate(_team);
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