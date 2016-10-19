using System;
using System.Collections.Generic;
using Patterns.Builder;

namespace EsportshubApi.Models.Entities 
{
    public interface IPlayerBuilder : IBuilder<Player>
    {
        IPlayerBuilder SetPlayerId(int input);
        IPlayerBuilder SetPlayerGuid(Guid input);
        IPlayerBuilder SetNickname(string input);
        IPlayerBuilder SetAccount(Account input);
        IPlayerBuilder SetFollowers(List<Player> input);
        IPlayerBuilder SetGames(List<Game> input);
        IPlayerBuilder SetTeams(List<Team> input);
        IPlayerBuilder SetActivities(List<Activity> input);
        IPlayerBuilder SetGroups(List<Group> input);
        IPlayerBuilder SetIntegrations(List<Integration> input);
    }

}