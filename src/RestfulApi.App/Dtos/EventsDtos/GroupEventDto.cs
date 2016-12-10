using System;
using RestfulApi.App.Dtos.GroupDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class GroupEventDto : EventDto
    {
        public int GroupEventDtoId { get; set; }
        public Guid GroupEventDtoGuid { get; set; }
        public EventDto Event { get; set; }
        public GroupDto Group { get; set; }
    }
}