using System;
using Patterns.Builder;

namespace Models.Entities
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