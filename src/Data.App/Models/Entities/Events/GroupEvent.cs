using System;
using Data.App.Extensions.Entities;
using Data.App.Models.Esportshub.Builders.EventBuilders.GroupEventBuilders;

namespace Data.App.Models.Esportshub.Entities.Events
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