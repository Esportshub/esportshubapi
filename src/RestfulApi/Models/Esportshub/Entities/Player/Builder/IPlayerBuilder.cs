using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities.mappings;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        IPlayerBuilder SetPlayerId(int input);
        IPlayerBuilder SetPlayerGuid(Guid input);
        IPlayerBuilder SetNickname(string input);
        IPlayerBuilder SetAccount(ApplicationUser input);
        IPlayerBuilder SetFollowers(List<Player> input);
        IPlayerBuilder SetPlayerGames(List<PlayerGames> input);
        IPlayerBuilder SetPlayerTeams(List<PlayerTeams> input);
        IPlayerBuilder SetActivities(List<Activity> input);
        IPlayerBuilder SetPlayerGroups(List<PlayerGroups> input);
        IPlayerBuilder SetIntegrations(List<Integration> input);
    }

}