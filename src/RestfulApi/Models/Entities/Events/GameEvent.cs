using System;
using EsportshubApi.Extensions;

namespace EsportshubApi.Models.Entities
{
    public class GameEvent : Event
    {
        private GameEvent() { }

        public static GameEventBuilder Builder()
        {
            return new GameEventBuilder(new GameEvent());
        }
        public int GameEventId { get; set; }
        public Guid GameEventGuid { get; set; }
        public Event Event { get; set; }
        public Game Game { get; set; }


        public override bool Equals(object obj)
        {
            GameEvent gameEvent = (GameEvent)obj;

            if (this.CompareEntities(obj))
                return gameEvent.GameEventId == this.GameEventId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}