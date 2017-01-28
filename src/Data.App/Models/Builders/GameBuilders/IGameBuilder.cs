using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.GameBuilders
{
    public interface IGameBuilder : IBuilder<Game>
    {
        IGameBuilder SetName(string name);
        IGameBuilder SetGameGuid(Guid gameGuid);
        IGameBuilder SetTeams(List<Team> teams);
        IGameBuilder SetEvents(List<Event> events);
        IGameBuilder SetPlayerGames(List<PlayerGames> playerGames);
    }

}