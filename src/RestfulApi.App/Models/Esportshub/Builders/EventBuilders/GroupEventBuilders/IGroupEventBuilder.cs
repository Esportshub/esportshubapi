using System;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;

namespace RestfulApi.App.Models.Esportshub.Builders.EventBuilders.GroupEventBuilders
{
    public interface IGroupEventBuilder : IBuilder<GroupEvent>
    {
        IGroupEventBuilder SetGroupEventId(int input);
        IGroupEventBuilder SetGroupEventGuid(Guid input);
        IGroupEventBuilder SetEvent(Event input);
        IGroupEventBuilder SetGroup(Group input);

    }
}