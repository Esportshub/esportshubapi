using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.PlayerBuilders
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        IPlayerBuilder SetPlayerGuid(Guid input);
        IPlayerBuilder SetPlayerId(int input);
        IPlayerBuilder SetNickname(string input);
        IPlayerBuilder SetFollowers(List<Player> input);
        IPlayerBuilder SetPlayerGames(List<PlayerGames> input);
        IPlayerBuilder SetPlayerTeams(List<PlayerTeams> input);
        IPlayerBuilder SetActivities(List<Activity> input);
        IPlayerBuilder SetPlayerGroups(List<PlayerGroups> input);
        IPlayerBuilder SetIntegrations(List<Integration> input);
    }

}