using System;

namespace Models.Entities 
{
    public class GroupEvent : Event
    {
        public int GroupEventId { get; set; }
        public Guid GroupEventGuid { get; set; }
        public Event Event { get; set; }
        public Group Group { get; set; }
    }
}