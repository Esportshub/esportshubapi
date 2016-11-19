using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities.mappings;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IGameBuilder : IBuilder<Game,GameValidator>
    {
        IGameBuilder SetGameId(int input);
        IGameBuilder SetName(string input);
        IGameBuilder SetGameGuid(Guid input);
        IGameBuilder SetCreated(DateTime input);
        IGameBuilder SetUpdated(DateTime input);
        IGameBuilder SetTeams(List<Team> input);
        IGameBuilder SetGameEvents(List<GameEvent> input);
        IGameBuilder SetPlayerGames(List<PlayerGames> input);
    }

}