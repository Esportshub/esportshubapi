using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.GameBuilders
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