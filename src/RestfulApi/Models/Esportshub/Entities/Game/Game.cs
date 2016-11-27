using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using RestfulApi.Extensions.Entities;
using RestfulApi.Models.Esportshub.Entities;
using RestfulApi.Models.Esportshub.Entities.mappings;
using RestfulApi.Models.Esportshub.Entities.Team;

namespace EsportshubApi.Models.Entities
{
    public class Game : EsportshubEntity
    {
        private Game() { }

        public static GameBuilder Builder()
        {
            return new GameBuilder(new Game());
        }

        public int GameId { get; private set; }

        public string Name { get; set; }

        public Guid GameGuid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated { get; private set; }

        public List<Team> Teams { get; set; }

        public List<GameEvent> GameEvents { get; set; }

        public List<PlayerGames> PlayerGames { get; set; }

        [NotMapped]
        public int Id => GameId;

        public override bool Equals(object obj)
        {
            Game game = (Game)obj;

            if (this.CompareEntities(obj))
                return game.GameId == this.GameId;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}