using System;

namespace Models.Entities 
{
    public class GameEvent : Event
    {
        public int GameEventId { get; set; }
        public Guid GameEventGuid { get; set; }
        public Event Event { get; set; }
        public Game Game { get; set; }
    }
}