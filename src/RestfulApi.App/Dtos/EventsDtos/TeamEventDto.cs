using System;
using RestfulApi.App.Dtos.TeamDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class TeamEventDto: EventDto
    {
        public int TeamEventId { get; set; }
        public Guid TeamEventGuid { get; set; }
        public EventDto Event { get; set; }
        public TeamDto Team { get; set; }

    }
}