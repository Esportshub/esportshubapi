using System;
using Patterns.Builder;

namespace Models.Entities
{
    public interface IGameEventBuilder : IBuilder<GameEvent>
    { 

        IGameEventBuilder SetGameEventId(int input);
        IGameEventBuilder SetGameEventGuid(Guid input);
        IGameEventBuilder SetEvent(Event input);
        IGameEventBuilder SetGame(Game input);

    }
}