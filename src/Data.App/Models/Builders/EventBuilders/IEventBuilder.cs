using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.EventBuilders
{
    public interface IEventBuilder : IBuilder<Event>
    { 
       IEventBuilder  SetEventId(int eventId);
       IEventBuilder  SetName(string name);
       IEventBuilder  SetEventGuid(Guid eventGuid);
       IEventBuilder  SetStart(DateTime start);
       IEventBuilder  SetEnd(DateTime end);
       IEventBuilder  SetCreated(DateTime created);
       IEventBuilder  SetUpdated(DateTime updated);
    }
}