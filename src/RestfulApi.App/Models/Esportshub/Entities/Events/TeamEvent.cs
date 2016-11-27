using System;
using RestfulApi.App.Extensions.Entities;
using RestfulApi.App.Models.Esportshub.Builders.EventBuilders.TeamEventBuilders;

namespace RestfulApi.App.Models.Esportshub.Entities.Events
{
    public class TeamEvent : Event
    {
        private TeamEvent() { }

        public static TeamEventBuilder Builder()
        {
            return new TeamEventBuilder(new TeamEvent());
        }

        public int TeamEventId { get; set; }
        public Guid TeamEventGuid { get; set; }
        public Event Event { get; set; }
        public Team Team { get; set; }

        public override bool Equals(object obj)
        {
            TeamEvent teamEvent = (TeamEvent)obj;

            if (this.CompareEntities(obj))
                return teamEvent.TeamEventId == this.TeamEventId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}