using System;
using System.Collections.Generic;
using Patterns.Builder;

namespace Models.Entities
{
    public interface IGameBuilder : IBuilder<Game>
    {
        IGameBuilder SetGameId(int input);
        IGameBuilder SetName(string input);
        IGameBuilder SetGameGuid(Guid input);
        IGameBuilder SetCreated(DateTime input);
        IGameBuilder SetUpdated(DateTime input);
        IGameBuilder SetTeams(List<Team> input);
        IGameBuilder SetGameEvents(List<Event> input);
        IGameBuilder SetPlayers(List<Player> input);

    }

}