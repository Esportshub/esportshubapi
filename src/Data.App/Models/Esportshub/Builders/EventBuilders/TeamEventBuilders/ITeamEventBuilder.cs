using System;
using Data.App.Models.Esportshub.Entities;
using Data.App.Models.Esportshub.Entities.Events;

namespace Data.App.Models.Esportshub.Builders.EventBuilders.TeamEventBuilders
{
    public interface ITeamEventBuilder : IBuilder<TeamEvent>
    { 
        ITeamEventBuilder SetTeamEventId(int input);
        ITeamEventBuilder SetTeamEventGuid(Guid input);
        ITeamEventBuilder SetEvent(Event input);
        ITeamEventBuilder Setteam(Team input);

        

    }
}