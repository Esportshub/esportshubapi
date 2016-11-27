using System;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IGameEventBuilder : IBuilder<GameEvent,GameEventValidator>
    { 

        IGameEventBuilder SetGameEventId(int input);
        IGameEventBuilder SetGameEventGuid(Guid input);
        IGameEventBuilder SetEvent(Event input);
        IGameEventBuilder SetGame(Game input);

    }
}