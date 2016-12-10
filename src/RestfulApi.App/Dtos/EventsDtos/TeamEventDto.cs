using System;
using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class TeamEventDto: EventDto
    {
        public int TeamEventDtoId { get; set; }
        public Guid TeamEventDtoGuid { get; set; }
        public EventDto EventDto { get; set; }
        public TeamDto TeamDto { get; set; }

    }
}