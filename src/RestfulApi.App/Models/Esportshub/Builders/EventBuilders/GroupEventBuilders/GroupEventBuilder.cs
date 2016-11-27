using System;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;

namespace RestfulApi.App.Models.Esportshub.Builders.EventBuilders.GroupEventBuilders
{
    public class GroupEventBuilder : IGroupEventBuilder
    {
        private GroupEvent _groupEvent;

        public GroupEventBuilder (GroupEvent groupEvent)
        {
          _groupEvent = groupEvent;
        }

        public GroupEvent Build()
        {
           return _groupEvent;
        }


        public IGroupEventBuilder SetEvent(Event input)
        {
           _groupEvent.Event = input;
           return this;
        }

        public IGroupEventBuilder SetGroup(Group input)
        {
           _groupEvent.Group = input;
           return this;
        }

        public IGroupEventBuilder SetGroupEventGuid(Guid input)
        {
            _groupEvent.GroupEventGuid = input;
            return this;
        }

        public IGroupEventBuilder SetGroupEventId(int input)
        {
            _groupEvent.EventId = input;
            return this;
        }
    }
}