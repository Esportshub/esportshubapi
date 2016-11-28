using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;
using RestfulApi.App.Models.Identity.Entities;

namespace RestfulApi.App.Models.Esportshub.Builders.PlayerBuilders
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        IPlayerBuilder SetPlayerGuid(Guid input);
        IPlayerBuilder SetPlayerId(int input);
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