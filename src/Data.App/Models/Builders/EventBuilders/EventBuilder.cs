using System;
using Data.App.Models.Entities.Events;

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


        public IEventBuilder SetCreated(DateTime input)
        {
            _event.Created = input;
            return this;
        }
        public IEventBuilder SetEnd(DateTime input)
        {
            _event.End = input;
            return this;
        }

        public IEventBuilder SetEventGuid(Guid input)
        {
            _event.EventGuid = input;
            return this;
        }

        public IEventBuilder SetEventId(int input)
        {
            _event.EventId = input;
            return this;
        }

        public IEventBuilder SetName(string input)
        {
            _event.Name = input;
            return this;
        }

        public IEventBuilder SetStart(DateTime input)
        {
            _event.Start = input;
            return this;
        }

        public IEventBuilder SetUpdated(DateTime input)
        {
            _event.Updated = input;
            return this;
        }
    }
}