using System;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;

namespace RestfulApi.App.Models.Esportshub.Builders.EventBuilders.GameEventBuilders
{
    public interface IGameEventBuilder : IBuilder<GameEvent>
    { 

        IGameEventBuilder SetGameEventId(int input);
        IGameEventBuilder SetGameEventGuid(Guid input);
        IGameEventBuilder SetEvent(Event input);
        IGameEventBuilder SetGame(Game input);

    }
}