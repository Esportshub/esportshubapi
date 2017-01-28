using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.TeamBuilders
{
    public interface ITeamBuilder : IBuilder<Team>
    {
        ITeamBuilder TeamGuid(Guid guid);
        ITeamBuilder SetName(string name);
        ITeamBuilder SetGame(Game game);
        ITeamBuilder SetTeamEvents(List<Event> events);
        ITeamBuilder SetPlayerTeams(List<PlayerTeams> playerTeams);
    }

}