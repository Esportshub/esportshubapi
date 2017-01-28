using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.EsportshubEventBuilders
{
    public class EsportshubEventBuilder : IEsportshubEventBuilder
    {
        private readonly EsportshubEvent _esportshubEvent;

        public EsportshubEventBuilder (EsportshubEvent @event)
        {
          _esportshubEvent = @event;
        }

        public EsportshubEvent Build()
        {
            return _esportshubEvent;
        }

        public IEsportshubEventBuilder SetEsportshubEventGuid(Guid eventGuid)
        {
            _esportshubEvent.EventGuid = eventGuid;
            return this;
        }

        public IEsportshubEventBuilder SetEsportshubEventId(int eventId)
        {
            _esportshubEvent.EventId = eventId;
            return this;
        }

        public IEsportshubEventBuilder SetName(string name)
        {
            _esportshubEvent.Name = name;
            return this;
        }

        public IEsportshubEventBuilder SetStart(DateTime start)
        {
            _esportshubEvent.Start = start;
            return this;
        }

        public IEsportshubEventBuilder SetEnd(DateTime end)
        {
            _esportshubEvent.End = end;
            return this;
        }

        public IEsportshubEventBuilder SetCreated(DateTime created)
        {
            _esportshubEvent.Created = created;
            return this;
        }

        public IEsportshubEventBuilder SetUpdated(DateTime updated)
        {
            _esportshubEvent.Updated = updated;
            return this;
        }

    }
}