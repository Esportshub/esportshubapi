using System;

namespace RestfulApi.App.Dtos.EventsDtos
{
    public abstract class EventDto
    {

        public int EventDtoId { get; set; }
        public Guid EventDtoGuid { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}