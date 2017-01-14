using System;
using Data.App.Extensions.Entities;
using Data.App.Models.Esportshub.Builders.EventBuilders.GameEventBuilders;

namespace Data.App.Models.Esportshub.Entities.Events
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