using System;

namespace EsportshubApi.Models.Entities
{
    public class TeamEventBuilder : ITeamEventBuilder
    {
        private TeamEvent _teamEvent;

        public TeamEventBuilder (TeamEvent teamEvent)
        {
          _teamEvent = teamEvent;
        }

        public TeamEvent Build()
        {
           return _teamEvent;
        }

        public ITeamEventBuilder SetEvent(Event input)
        {
            _teamEvent.Event = input;
            return this;
        }

        public ITeamEventBuilder Setteam(Team input)
        {
            _teamEvent.Team = input;
            return this;
        }

        public ITeamEventBuilder SetTeamEventGuid(Guid input)
        {
            _teamEvent.EventGuid = input;
            return this;
        }

        public ITeamEventBuilder SetTeamEventId(int input)
        {
            _teamEvent.EventId = input;
            return this;
        }
    }
}