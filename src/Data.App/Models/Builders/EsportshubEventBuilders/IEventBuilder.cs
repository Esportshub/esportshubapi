using System;
using Data.App.Models.Entities;

namespace Data.App.Models.Builders.EsportshubEventBuilders
{
    public interface IEsportshubEventBuilder : IBuilder<EsportshubEvent>
    { 
       IEsportshubEventBuilder  SetEsportshubEventId(int esportshubEventId);
       IEsportshubEventBuilder  SetName(string name);
       IEsportshubEventBuilder  SetEsportshubEventGuid(Guid esportshubEventGuid);
       IEsportshubEventBuilder  SetStart(DateTime start);
       IEsportshubEventBuilder  SetEnd(DateTime end);
       IEsportshubEventBuilder  SetCreated(DateTime created);
       IEsportshubEventBuilder  SetUpdated(DateTime updated);
    }
}