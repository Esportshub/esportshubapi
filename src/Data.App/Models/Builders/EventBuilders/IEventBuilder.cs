using System;
using Data.App.Models.Entities.Events;

namespace Data.App.Models.Builders.EventBuilders
{
    public interface IEventBuilder : IBuilder<Event>
    { 

       IEventBuilder  SetEventId(int input);
       IEventBuilder  SetName(string input);
       IEventBuilder  SetEventGuid(Guid input);
       IEventBuilder  SetStart(DateTime input);
       IEventBuilder  SetEnd(DateTime input);
       IEventBuilder  SetCreated(DateTime input);
       IEventBuilder  SetUpdated(DateTime input);

    }
}