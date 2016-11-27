using System;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;

namespace RestfulApi.App.Models.Esportshub.Builders.EventBuilders.TeamEventBuilders
{
    public interface ITeamEventBuilder : IBuilder<TeamEvent>
    { 
        ITeamEventBuilder SetTeamEventId(int input);
        ITeamEventBuilder SetTeamEventGuid(Guid input);
        ITeamEventBuilder SetEvent(Event input);
        ITeamEventBuilder Setteam(Team input);

        

    }
}