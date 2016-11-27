using System;
using RestfulApi.Models.Esportshub.Entities.Team;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface ITeamEventBuilder : IBuilder<TeamEvent,TeamEventValidator>
    { 
        ITeamEventBuilder SetTeamEventId(int input);
        ITeamEventBuilder SetTeamEventGuid(Guid input);
        ITeamEventBuilder SetEvent(Event input);
        ITeamEventBuilder Setteam(Team input);

        

    }
}