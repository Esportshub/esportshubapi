using System;
using RestfulApi.Models.Esportshub.Entities.Group;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IGroupEventBuilder : IBuilder<GroupEvent,GroupEventValidator>
    { 
        IGroupEventBuilder SetGroupEventId(int input);
        IGroupEventBuilder SetGroupEventGuid(Guid input);
        IGroupEventBuilder SetEvent(Event input);
        IGroupEventBuilder SetGroup(Group input);

    }
}