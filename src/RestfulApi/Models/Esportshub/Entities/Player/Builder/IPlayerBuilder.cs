using System;
using System.Collections.Generic;
using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Models.Esportshub.Entities.Player.Validator;
using RestfulApi.Patterns.Builder;

namespace RestfulApi.Models.Esportshub.Entities.Player.Builder
{
    public interface IPlayerBuilder : IBuilder<Player, PlayerValidator>
    {
        IPlayerBuilder SetPlayerGuid(Guid input);
        IPlayerBuilder SetNickname(string input);
        IPlayerBuilder SetAccount(ApplicationUser input);
        IPlayerBuilder SetFollowers(List<Player> input);
        IPlayerBuilder SetPlayerGames(List<PlayerGames> input);
        IPlayerBuilder SetPlayerTeams(List<PlayerTeams> input);
        IPlayerBuilder SetActivities(List<Activity> input);
        IPlayerBuilder SetPlayerGroups(List<PlayerGroups> input);
        IPlayerBuilder SetIntegrations(List<Integration.Integration> input);
    }

}