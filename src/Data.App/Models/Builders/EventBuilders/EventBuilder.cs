using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.EventBuilders
{
    public class EventBuilder : IEventBuilder
    {
        private Event _event;

        public EventBuilder (Event @event)
        {
          _event = @event;
        }

        public Event Build()
        {
            return _event;
        }

        public IEventBuilder SetEventGuid(Guid eventGuid)
        {
            _event.EventGuid = eventGuid;
            return this;
        }

        public IEventBuilder SetEventId(int eventId)
        {
            _event.EventId = eventId;
            return this;
        }

        public IEventBuilder SetName(string name)
        {
            _event.Name = name;
            return this;
        }

        public IEventBuilder SetStart(DateTime start)
        {
            _event.Start = start;
            return this;
        }

        public IEventBuilder SetEnd(DateTime end)
        {
            _event.End = end;
            return this;
        }

        public IEventBuilder SetCreated(DateTime created)
        {
            _event.Created = created;
            return this;
        }

        public IEventBuilder SetUpdated(DateTime updated)
        {
            _event.Updated = updated;
            return this;
        }

    }
}