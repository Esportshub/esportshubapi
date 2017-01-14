using System;
using System.Collections.Generic;
using Data.App.Models.Esportshub.Entities;
using Data.App.Models.Esportshub.Entities.Events;
using Data.App.Models.Esportshub.Entities.Mappings;

namespace Data.App.Models.Esportshub.Builders.GameBuilders
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