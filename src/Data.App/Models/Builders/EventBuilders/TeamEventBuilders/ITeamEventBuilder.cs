using System;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;

namespace Data.App.Models.Builders.EventBuilders.TeamEventBuilders
{
    public interface ITeamEventBuilder : IBuilder<TeamEvent>
    { 
        ITeamEventBuilder SetTeamEventId(int input);
        ITeamEventBuilder SetTeamEventGuid(Guid input);
        ITeamEventBuilder SetEvent(Event input);
        ITeamEventBuilder Setteam(Team input);

        

    }
}