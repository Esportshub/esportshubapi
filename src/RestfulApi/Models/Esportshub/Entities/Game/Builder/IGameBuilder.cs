using System;
using System.Collections.Generic;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Models.Esportshub.Entities.Team;
using RestfulApi.Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface IGameBuilder : IBuilder<Game,GameValidator>
    {
        IGameBuilder SetName(string input);
        IGameBuilder SetGameGuid(Guid input);
        IGameBuilder SetTeams(List<Team> input);
        IGameBuilder SetGameEvents(List<GameEvent> input);
        IGameBuilder SetPlayerGames(List<PlayerGames> input);
    }

}