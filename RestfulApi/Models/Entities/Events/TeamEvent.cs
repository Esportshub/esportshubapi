using System;

namespace Models.Entities 
{
    public class TeamEvent : Event
    {
        public int TeamEventId { get; set; }
        public Guid TeamEventGuid { get; set; }
        public Event Event { get; set; }
        public Team Team { get; set; }
    }
}