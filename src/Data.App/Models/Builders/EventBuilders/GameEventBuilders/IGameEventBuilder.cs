using System;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;

namespace Data.App.Models.Builders.EventBuilders.GameEventBuilders
{
    public interface IGameEventBuilder : IBuilder<GameEvent>
    { 

        IGameEventBuilder SetGameEventId(int input);
        IGameEventBuilder SetGameEventGuid(Guid input);
        IGameEventBuilder SetEvent(Event input);
        IGameEventBuilder SetGame(Game input);

    }
}