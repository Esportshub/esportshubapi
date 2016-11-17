using System;
using System.Collections.Generic;
using RestfulApi.Models.Validator;

namespace EsportshubApi.Models.Entities 
{
    public class PlayerBuilder : IPlayerBuilder
    {
        private Player _player;

        public PlayerBuilder() {
            _player = new Player();
        }

        public Player Build()
        {
           return _player;
        }

        public Player Build(IValidator<Player> validator)
        {
            throw new NotImplementedException();
        }

        public Player Build(PlayerValidator validator)
        {
            validator.Validate(_player);
           return _player;
        }

        //@TODO: Throw the correct exceptions/validate correctly

        public IPlayerBuilder SetAccount(Account input)
        {
            _player.Account = input;
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

        public IPlayerBuilder SetGames(List<Game> input)
        {
             _player.Games = input;
             return this;
        }

        public IPlayerBuilder SetGroups(List<Group> input)
        {
             _player.Groups = input;
             return this;
        }

        public IPlayerBuilder SetIntegrations(List<Integration> input)
        {
             _player.Integrations = input;
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

        public IPlayerBuilder SetPlayerId(int input)
        {
             _player.PlayerId = input;
             return this;
        }

        public IPlayerBuilder SetTeams(List<Team> input)
        {
             _player.Teams = input;
             return this;
        }
    }
}