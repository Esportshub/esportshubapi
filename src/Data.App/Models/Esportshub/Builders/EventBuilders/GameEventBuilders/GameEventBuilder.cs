using System;
using Data.App.Models.Esportshub.Entities;
using Data.App.Models.Esportshub.Entities.Events;

namespace Data.App.Models.Esportshub.Builders.EventBuilders.GameEventBuilders
{
    public class GameEventBuilder : IGameEventBuilder
    {
        private GameEvent _gameEvent;

        public GameEventBuilder (GameEvent gameEvent)
        {
            _gameEvent = gameEvent;
        }
        public GameEvent Build()
        {
            return _gameEvent;
        }


        public IGameEventBuilder SetEvent(Event input)
        {
          _gameEvent.Event = input;
          return this;
        }

        public IGameEventBuilder SetGame(Game input)
        {
           _gameEvent.Game = input;
           return this;
        }

        public IGameEventBuilder SetGameEventGuid(Guid input)
        {
            _gameEvent.EventGuid = input;
            return this;
        }

        public IGameEventBuilder SetGameEventId(int input)
        {
            _gameEvent.EventId = input;
            return this;
        }
    }
}