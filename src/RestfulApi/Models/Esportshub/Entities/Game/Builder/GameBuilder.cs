using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EsportshubApi.Models.Entities.mappings;
using Patterns.Builder;
using RestfulApi.Models.Validator;

namespace EsportshubApi.Models.Entities
{
    public class GameBuilder : IGameBuilder
    {
        private Game _game;

        public GameBuilder(Game game)
        {
            _game = game;
        }

        public Game Build(IValidator<Game> validator)
        {
            validator.Validate(_game);
            return _game;
        }

        public Game Build()
        {
            return _game;
        }


        public IGameBuilder SetCreated(DateTime input)
        {
            _game.Created = input;
            return this;
        }

        public IGameBuilder SetGameEvents(List<Event> input)
        {
            throw new NotImplementedException();
        }

        public IGameBuilder SetGameEvents(List<GameEvent> input)
        {
            _game.GameEvents = input;
            return this;
        }

        public IGameBuilder SetGameGuid(Guid input)
        {
            _game.GameGuid = input;
            return this;
        }

        public IGameBuilder SetGameId(int input)
        {
            _game.GameId = input;
            return this;
        }

        public IGameBuilder SetName(string input)
        {
            _game.Name = input;
            return this;
        }

        public IGameBuilder SetPlayerGames(List<PlayerGames> input)
        {
            _game.PlayerGames = input;
            return this;
        }

        public IGameBuilder SetTeams(List<Team> input)
        {
            _game.Teams = input;
            return this;
        }

        public IGameBuilder SetUpdated(DateTime input)
        {
            _game.Updated = input;
            return this;
        }
    }
}