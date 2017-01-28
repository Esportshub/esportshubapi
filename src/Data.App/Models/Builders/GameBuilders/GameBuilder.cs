using System;
using System.Collections.Generic;
using Data.App.Models.Entities;
using Data.App.Models.Entities.Mappings;

namespace Data.App.Models.Builders.GameBuilders
{
    public class GameBuilder : IGameBuilder
    {
        private Game _game;

        public GameBuilder(Game game)
        {
            _game = game;
        }

        public Game Build()
        {
            return _game;
        }

        public IGameBuilder SetEsportshubEvents(List<EsportshubEvent> events)
        {
            _game.EsportshubEvents = events;
            return this;
        }

        public IGameBuilder SetGameGuid(Guid gameGuid)
        {
            _game.GameGuid = gameGuid;
            return this;
        }

        public IGameBuilder SetName(string name)
        {
            _game.Name = name;
            return this;
        }

        public IGameBuilder SetPlayerGames(List<PlayerGames> playerGames)
        {
            _game.PlayerGames = playerGames;
            return this;
        }

        public IGameBuilder SetTeams(List<Team> teams)
        {
            _game.Teams = teams;
            return this;
        }
    }
}