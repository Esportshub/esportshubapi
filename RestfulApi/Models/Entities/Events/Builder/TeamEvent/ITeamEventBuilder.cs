using System;
using Patterns.Builder;

namespace Models.Entities
{
    public interface ITeamEventBuilder : IBuilder<TeamEvent>
    { 
        ITeamEventBuilder SetTeamEventId(int input);
        ITeamEventBuilder SetTeamEventGuid(Guid input);
        ITeamEventBuilder SetEvent(Event input);
        ITeamEventBuilder Setteam(Team input);

        

    }
}