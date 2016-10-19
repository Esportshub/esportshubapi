using System;
using System.Collections.Generic;
using Models.Entities;
using Patterns.Builder;

namespace Models.Entities 
{
    public class PlayerBuilder : IPlayerBuilder
    {
        private Player _player;

        public PlayerBuilder() {
            _player = new Player();
        }

        //@TODO: Throw the correct exceptions/validate correctly
        public Player Build()
        {
            return _player;
        }

        public IPlayerBuilder SetAccount(Account input)
        {
            _player.Account = input;
            return this;
        }

        public IPlayerBuilder SetActivities(List<Activity> input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetFollowers(List<Player> input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetGames(List<Game> input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetGroups(List<Group> input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetIntegrations(List<Integration> input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetNickname(string input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetPlayerGuid(Guid input)
        {
            return this;
        }

        public IPlayerBuilder SetPlayerId(int input)
        {
            throw new NotImplementedException();
        }

        public IPlayerBuilder SetTeams(List<Team> input)
        {
            throw new NotImplementedException();
        }
    }
}