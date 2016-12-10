using System;
using RestfulApi.App.Dtos.GameDtos;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public class GameEventDto : EventDto
    {
        public int GameEventDtoId { get; set; }
        public Guid GameEventDtoGuid { get; set; }
        public EventDto EventDto { get; set; }
        public GameDto GameDto { get; set; }
    }
}