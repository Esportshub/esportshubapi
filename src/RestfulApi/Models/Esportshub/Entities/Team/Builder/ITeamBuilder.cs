using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Patterns.Builder;

namespace RestfulApi.Models.Esportshub.Entities.Team.Builder
{
    public interface ITeamBuilder : IBuilder<Team, TeamValidator>
    {
        ITeamBuilder TeamGuid(Guid input);
        ITeamBuilder SetName(string input);
        ITeamBuilder SetGame(Game input);      
        ITeamBuilder SetTeamEvents(List<TeamEvent> input);
        ITeamBuilder SetPlayerTeams(List<PlayerTeams> input);
    }

}