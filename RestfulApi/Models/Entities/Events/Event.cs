using System;
using Extensions.Entities;

namespace Models.Entities
{
    public class Event
    {
        // private Event() { }

        public static EventBuilder Builder()
        {
            return new EventBuilder(new Event());
        }

        public int EventId { get; set; }
        public string Name { get; set; }
        public Guid EventGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public override bool Equals(object obj)
        {
            Event @event = (Event)obj;

            if (this.CompareEntities(obj))
                return @event.EventId == this.EventId;

            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}