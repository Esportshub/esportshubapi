using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Esportshub.Entities.Mappings;
using RestfulApi.App.Models.Identity.Entities;

namespace RestfulApi.App.Models.Esportshub.Builders.PlayerBuilders
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


        //@TODO: Throw the correct exceptions/validate correctly

        public IPlayerBuilder SetAccount(ApplicationUser input)
        {
            _player.ApplicationUser = input;
            return this;
        }

        public IPlayerBuilder SetActivities(List<Activity> input)
        {
            _player.Activities = input;
            return this;
        }

        public IPlayerBuilder SetFollowers(List<Player> input)
        {
            _player.Followers = input;
            return this;
        }

        public IPlayerBuilder SetPlayerGames(List<PlayerGames> input)
        {
            _player.PlayerGames = input;
            return this;
        }

        public IPlayerBuilder SetPlayerGroups(List<PlayerGroups> input)
        {
            _player.PlayerGroups = input;
            return this;
        }

        public IPlayerBuilder SetIntegrations(List<Integration> input)
        {
            _player.Integrations = input;
            return this;
        }

        public IPlayerBuilder SetPlayerId(int input)
        {
            _player.PlayerId = input;
            return this;
        }


        public IPlayerBuilder SetNickname(string input)
        {
            _player.Nickname = input;
            return this;
        }

        public IPlayerBuilder SetPlayerGuid(Guid input)
        {
            _player.PlayerGuid = input;
            return this;
        }

        public IPlayerBuilder SetPlayerTeams(List<PlayerTeams> input)
        {
            _player.PlayerTeams = input;
            return this;
        }
    }
}