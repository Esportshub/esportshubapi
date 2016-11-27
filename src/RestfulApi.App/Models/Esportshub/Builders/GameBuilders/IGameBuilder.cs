using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Events;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;

namespace RestfulApi.App.Models.Esportshub.Builders.GameBuilders
{
    public interface IGameBuilder : IBuilder<Game>
    {
        IGameBuilder SetName(string input);
        IGameBuilder SetGameGuid(Guid input);
        IGameBuilder SetTeams(List<Team> input);
        IGameBuilder SetGameEvents(List<GameEvent> input);
        IGameBuilder SetPlayerGames(List<PlayerGames> input);
    }

}