using System;
using RestfulApi.Extensions.Entities;
using RestfulApi.Models.Esportshub.Entities.Group;

namespace EsportshubApi.Models.Entities
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