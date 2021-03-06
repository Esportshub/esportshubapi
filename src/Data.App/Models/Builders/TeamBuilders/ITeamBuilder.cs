using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.TeamBuilders
{
    public interface ITeamBuilder : IBuilder<Team>
    {
        ITeamBuilder SetTeamGuid(Guid guid);
        ITeamBuilder SetName(string name);
        ITeamBuilder SetGame(Game game);
        ITeamBuilder SetEsportshubEvents(List<EsportshubEvent> esportshubEvents);
        ITeamBuilder SetPlayerTeams(List<PlayerTeams> playerTeams);
    }

}