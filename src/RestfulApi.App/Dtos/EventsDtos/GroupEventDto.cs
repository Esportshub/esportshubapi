using System;
using RestfulApi.App.Dtos.GroupDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class GroupEventDto : EventDto
    {
        public int GroupEventId { get; set; }
        public Guid GroupEventGuid { get; set; }
        public EventDto Event { get; set; }
        public GroupDto Group { get; set; }
    }
}