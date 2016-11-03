using System;
using System.Collections.Generic;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities
{
    public interface ITeamBuilder : IBuilder<Team,TeamValidator>
    {
        ITeamBuilder TeamId(int input);
        ITeamBuilder TeamGuid(Guid input);
        ITeamBuilder SetName(string input);
        ITeamBuilder SetCreated(DateTime input);
        ITeamBuilder SetUpdated(DateTime input);
        ITeamBuilder SetGame(Group input);      
        ITeamBuilder SetEvents(List<Event> input);
        ITeamBuilder SetPlayers(List<Player> input);
    }

}