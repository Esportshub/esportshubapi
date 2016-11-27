using System;
using RestfulApi.App.Extensions.Entities;
using RestfulApi.App.Models.Esportshub.Builders.EventBuilders.GroupEventBuilders;

namespace RestfulApi.App.Models.Esportshub.Entities.Events
{
    public class GroupEvent : Event
    {
        private GroupEvent() { }

        public static GroupEventBuilder Builder()
        {
            return new GroupEventBuilder(new GroupEvent());
        }
        public int GroupEventId { get; set; }
        public Guid GroupEventGuid { get; set; }
        public Event Event { get; set; }
        public Group Group { get; set; }

        public override bool Equals(object obj)
        {
            GroupEvent groupEvent = (GroupEvent)obj;

            if (this.CompareEntities(obj))
                return groupEvent.GroupEventId == this.GroupEventId;

            return false;
        }
        
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}