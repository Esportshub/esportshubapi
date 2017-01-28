using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.PlayerBuilders
{

    public class PlayerBuilder : IPlayerBuilder
    {
        private Player _player;

        public PlayerBuilder(Player player) {
            _player = player;
        }

        public Player Build()
        {
            return _player;
        }

        public IPlayerBuilder SetActivities(List<Activity> activities)
        {
            _player.Activities = activities;
            return this;
        }

        public IPlayerBuilder SetFollowers(List<Player> followers)
        {
            _player.Followers = followers;
            return this;
        }

        public IPlayerBuilder SetPlayerGames(List<PlayerGames> playerGames)
        {
            _player.PlayerGames = playerGames;
            return this;
        }

        public IPlayerBuilder SetPlayerGroups(List<PlayerGroups> playerGroups)
        {
            _player.PlayerGroups = playerGroups;
            return this;
        }

        public IPlayerBuilder SetIntegrations(List<Integration> integrations)
        {
            _player.Integrations = integrations;
            return this;
        }

        public IPlayerBuilder SetPlayerId(int playerId)
        {
            _player.PlayerId = playerId;
            return this;
        }


        public IPlayerBuilder SetNickname(string nickname)
        {
            _player.Nickname = nickname;
            return this;
        }

        public IPlayerBuilder SetPlayerGuid(Guid guid)
        {
            _player.PlayerGuid = guid;
            return this;
        }

        public IPlayerBuilder SetPlayerTeams(List<PlayerTeams> playerTeams)
        {
            _player.PlayerTeams = playerTeams;
            return this;
        }
    }
}