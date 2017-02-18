using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.PlayerBuilders
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        IPlayerBuilder SetPlayerGuid(Guid guid);
        IPlayerBuilder SetPlayerId(int playerId);
        IPlayerBuilder SetNickname(string nickname);
        IPlayerBuilder SetFollowers(List<Player> followers);
        IPlayerBuilder SetPlayerGames(List<PlayerGames> playerGames);
        IPlayerBuilder SetPlayerTeams(List<PlayerTeams> playerTeams);
        IPlayerBuilder SetActivities(List<Activity> activities);
        IPlayerBuilder SetPlayerGroups(List<PlayerGroups> playerGroups);
        IPlayerBuilder SetIntegrations(List<Integration> integrations);
    }

}