using System;
using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class GameEventDto : EventDto
    {
        public int GameEventId { get; set; }
        public Guid GameEventGuid { get; set; }
        public EventDto Event { get; set; }
        public GameDto Game { get; set; }
    }
}