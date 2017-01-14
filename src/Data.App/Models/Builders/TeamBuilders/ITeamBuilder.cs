using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Events;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.TeamBuilders
{
    public interface ITeamBuilder : IBuilder<Team>
    {
        ITeamBuilder TeamGuid(Guid input);
        ITeamBuilder SetName(string input);
        ITeamBuilder SetGame(Game input);
        ITeamBuilder SetTeamEvents(List<TeamEvent> input);
        ITeamBuilder SetPlayerTeams(List<PlayerTeams> input);
    }

}